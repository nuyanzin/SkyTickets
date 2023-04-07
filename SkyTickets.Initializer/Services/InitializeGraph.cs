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
    public class InitializeGraph
    {
        private readonly IGraphRepository _graphRepository;
        private readonly IFlightStatsService _flightStatsService;

        public InitializeGraph(
            IGraphRepository graphRepository,
            IFlightStatsService flightStatsService)
        {
            _graphRepository = graphRepository;
            _flightStatsService = flightStatsService;
        }

        public async Task Run()
        {
            //var queryForLoadCountries =
            //    "LOAD CSV WITH HEADERS " +
            //    "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/countries.csv' " +
            //    "AS line " +
            //    "CREATE (:Country { code: line.code, name: line.name })";

            //await _graphRepository.ExecuteQueryAsync(queryForLoadCountries);

            //var queryForLoadAirports =
            //    "LOAD CSV WITH HEADERS " +
            //    "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/airports.csv' " +
            //    "AS line " +
            //    "CREATE (a:Airport { name: line.name, city: line.city, iata: line.iata, icao: line.icao }) " +
            //    "WITH a, line " +
            //    "MATCH (c:Country) " +
            //    "WHERE c.name = line.country " +
            //    "CREATE (a)-[:IS_LOCATED_IN]->(c)";

            //await _graphRepository.ExecuteQueryAsync(queryForLoadAirports);

            //var queryForLoadAirlines =
            //    "LOAD CSV WITH HEADERS " +
            //    "FROM 'https://raw.githubusercontent.com/nuyanzin/SkyTickets/master/SkyTickets.Initializer/Resources/airlines.csv' " +
            //    "AS line " +
            //    "CREATE (a:Airline { name: line.name, city: line.city, iata: line.iata, icao: line.icao }) " +
            //    "WITH a, line " +
            //    "MATCH (c:Country) " +
            //    "WHERE c.name = line.country " +
            //    "CREATE (a)-[:IS_INCORPORATED_IN]->(c)";

            //await _graphRepository.ExecuteQueryAsync(queryForLoadAirlines);

            var queryForLoadFlights =
                "LOAD CSV WITH HEADERS " +
                "FROM 'https://github.com/nuyanzin/SkyTickets/blob/master/SkyTickets.Initializer/Resources/flights.csv' " +
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
