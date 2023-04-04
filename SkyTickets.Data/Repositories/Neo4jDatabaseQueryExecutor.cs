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
    public class Neo4jDatabaseQueryExecutor : IDatabaseQueryExecutor
    {
        private readonly IDriver _driver;
        public Neo4jDatabaseQueryExecutor(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<IResultCursor> ExecuteQueryAsync<IResultCursor>(string query)
        {
            try
            {
                await using var session = _driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
                await session.ExecuteWriteAsync(async tx =>
                {
                    return await tx.RunAsync(query);
                });
                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
