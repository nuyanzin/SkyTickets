using Neo4j.Driver;
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
        public Neo4jRepository(IDatabaseQueryExecutor databaseQueryExecutor) 
        {
            _databaseQueryExecutor = databaseQueryExecutor;
        }

        public async Task ExecuteQueryAsync(string query)
        {
            await _databaseQueryExecutor.ExecuteQueryAsync<IResultCursor>(query);
        }

        public async Task GetPathsBetweenAirports()
        {
            var query = "MATCH p=((src:Airport{name: 'Beijing Capital International Airport'})-[*1..4]-(dest:Airport{name: 'John F Kennedy International Airport'})) " +
                "WHERE ALL (i in range(0, size(relationships(p))-2) WHERE (relationships(p)[i]).date < (relationships(p)[i+1]).date) " +
                "AND (relationships(p)[0]).date > '2017-11-07 00:00:00' AND (relationships(p)[0]).date < '2017-11-08 00:00:00' " +
                "RETURN p";
            await _databaseQueryExecutor.ExecuteReadQueryAsync(query);
            
        }
    }
}
