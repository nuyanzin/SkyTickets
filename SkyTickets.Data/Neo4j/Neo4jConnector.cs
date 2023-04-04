using Neo4j.Driver;
using SkyTickets.Domain.Configuration;

namespace SkyTickets.Data.Neo4j
{
    public class Neo4jContext
    {
        public static IDriver Connect(INeo4jSettings neo4JSettings)
        {
            return GraphDatabase.Driver(neo4JSettings.DatabaseUri, AuthTokens.Basic(neo4JSettings.UserName, neo4JSettings.Password));
        }
    }
}
