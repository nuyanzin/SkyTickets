using SkyTickets.Domain.Configuration;

namespace SkyTickets.Domain.Settings
{
    public class Neo4jSettings : INeo4jSettings
    {
        public string DatabaseUri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
