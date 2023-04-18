using Neo4j.Driver;
using SkyTickets.Data.Mappers;
using SkyTickets.Domain.Entities;
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
            await using var session = _driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
            try
            {
                return (IResultCursor)await session.ExecuteWriteAsync(async tx =>
                {
                    return await tx.RunAsync(query);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<IRecord>> ExecuteReadQueryAsync(string query)
        {
            
            await using var session = _driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
            try
            {
                return await session.ExecuteReadAsync(async tx =>
                {
                    return (await (await tx.RunAsync(query)).ToListAsync());
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
