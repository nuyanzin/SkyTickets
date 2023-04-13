using SkyTickets.Business.FlightStatsSevice;
using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Initializer.Services
{
    public class InitializeDatabases
    {
        private readonly IGraphRepository _graphRepository;
        private readonly IAirportsRepository _airportsRepository;
        private readonly IFlightStatsService _flightStatsService;
        private readonly IHttpClientFactory _httpClientFactory;

        public InitializeDatabases(
            IGraphRepository graphRepository,
            IFlightStatsService flightStatsService,
            IAirportsRepository airportsRepository,
            IHttpClientFactory httpClientFactory)
        {
            _graphRepository = graphRepository;
            _flightStatsService = flightStatsService;
            _airportsRepository = airportsRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            //await InitializeNeo4j();
            //await InitializeMongoDb();
            await _graphRepository.GetPathsBetweenAirports();
        }

        private async Task InitializeMongoDb()
        {
            await SetAirportsColletionToMongoDb();
        }

        private async Task SetAirportsColletionToMongoDb()
        {
            using var httpClient = _httpClientFactory.CreateClient(nameof(InitializeDatabases));
            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/airports.csv");
            var content = await response.Content.ReadAsStringAsync();
            
            var dataRows = content.Split('\n').Skip(1);
            var airports = new List<Airport>();
            foreach(var row in dataRows )
            {
                airports.Add(new Airport(row));
            }

            await _airportsRepository.CreateBatchAsync(airports.ToArray());
        }
        
        private async Task InitializeNeo4j()
        {
            var queryForLoadCountries =
                "LOAD CSV WITH HEADERS " +
                "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/countries.csv' " +
                "AS line " +
                "CREATE (:Country { code: line.code, name: line.name })";

            await _graphRepository.ExecuteQueryAsync(queryForLoadCountries);

            var queryForLoadAirports =
                "LOAD CSV WITH HEADERS " +
                "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/airports.csv' " +
                "AS line " +
                "CREATE (a:Airport { name: line.name, city: line.city, iata: line.iata, icao: line.icao }) " +
                "WITH a, line " +
                "MATCH (c:Country) " +
                "WHERE c.name = line.country " +
                "CREATE (a)-[:IS_LOCATED_IN]->(c)";

            await _graphRepository.ExecuteQueryAsync(queryForLoadAirports);

            var queryForLoadAirlines =
                "LOAD CSV WITH HEADERS " +
                "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/airlines.csv' " +
                "AS line " +
                "CREATE (a:Airline { name: line.name, city: line.city, iata: line.iata, icao: line.icao }) " +
                "WITH a, line " +
                "MATCH (c:Country) " +
                "WHERE c.name = line.country " +
                "CREATE (a)-[:IS_INCORPORATED_IN]->(c)";

            await _graphRepository.ExecuteQueryAsync(queryForLoadAirlines);

            var queryForLoadFlights =
                "LOAD CSV WITH HEADERS " +
                "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/flights.csv' " +
                "AS line " +
                "WITH line " +
                "LIMIT 750 " +
                "CREATE (flight:Flight { number: line.flight_number }) " +
                "WITH flight, line " +
                "MATCH (departure:Airport) " +
                "MATCH (arrival:Airport) " +
                "MATCH (airline:Airline) " +
                "WHERE departure.iata = line.departure_airport AND arrival.iata = line.arrival_airport " +
                "AND airline.iata = line.airline_code " +
                "CREATE (airline)-[:PROVIDES]->(flight), " +
                "(flight)-[:DEPARTS_AT { date: line.departure_time, timestamp: line.departure_timestamp }]->(departure), " +
                "(flight)-[:ARRIVES_AT { date: line.arrival_time, timestamp: line.arrival_timestamp}]->(arrival)";

            await _graphRepository.ExecuteQueryAsync(queryForLoadFlights);
        }
    }
}
