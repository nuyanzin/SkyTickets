using Neo4j.Driver;
using SkyTickets.Data.Mappers;
using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Queries;
using SkyTickets.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Data.Repositories
{
    public class Neo4jRepository : IGraphRepository
    {
        private readonly IDatabaseQueryExecutor _databaseQueryExecutor;
        private readonly IAirportsRepository _airportsRepository;

        public Neo4jRepository(
            IDatabaseQueryExecutor databaseQueryExecutor,
            IAirportsRepository airportsRepository) 
        {
            _databaseQueryExecutor = databaseQueryExecutor;
            _airportsRepository = airportsRepository;
        }

        public async Task ExecuteQueryAsync(string query)
        {
            await _databaseQueryExecutor.ExecuteQueryAsync<IResultCursor>(query);
        }

        public async Task<List<FlightPath>> GetPathsBetweenAirports(SimplePathQuery pathQuery)
        {
            var query = "MATCH p=((src:Airport{name: 'Beijing Capital International Airport'})-[*1..4]-(dest:Airport{name: 'John F Kennedy International Airport'})) " +
                "WHERE ALL (i in range(0, size(relationships(p))-2) WHERE (relationships(p)[i]).date < (relationships(p)[i+1]).date) " +
                "AND (relationships(p)[0]).date > '2017-11-07 00:00:00' AND (relationships(p)[0]).date < '2017-11-08 00:00:00' " +
                "RETURN p";
            var result = await _databaseQueryExecutor.ExecuteReadQueryAsync(query);
            return result.Select(record => FlightPathMapper.Map(record.Values["p"].As<IPath>())).ToList();

        }

        private async string BuildStringQuery(SimplePathQuery pathQuery)
        {
            if (pathQuery.Flight == null)
            {
                throw new ArgumentNullException(nameof(pathQuery.Flight));
            }

            var departureAirport = await _airportsRepository.GetAsync(pathQuery.Flight.DepartureAirportEntityId);
            var arrivalAirport = await _airportsRepository.GetAsync(pathQuery.Flight.ArrivalAirportEntityId);

            var departureAirportIdentifier = departureAirport.GetNotNullIdentifier();
            var arrivalAirportIdentifier = arrivalAirport.GetNotNullIdentifier();

            var query = 
                $"MATCH p=((src:Airport{{{departureAirportIdentifier.Key}: '{departureAirportIdentifier.Value}'}})" +
                $"-[*1..4]-" +
                $"(dest:Airport{{{a}: 'John F Kennedy International Airport'}})) " +
                "WHERE ALL (i in range(0, size(relationships(p))-2) WHERE (relationships(p)[i]).date < (relationships(p)[i+1]).date) " +
                "AND (relationships(p)[0]).date > '2017-11-07 00:00:00' AND (relationships(p)[0]).date < '2017-11-08 00:00:00' " +
                "RETURN p";
        }

        
    }
}
