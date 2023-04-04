using Neo4j.Driver;
using SkyTickets.Domain.Repositories;
using System;
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


    }
}
