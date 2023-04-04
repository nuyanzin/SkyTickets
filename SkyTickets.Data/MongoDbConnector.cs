using MongoDB.Driver;

namespace SkyTickets.Data
{
    public static class MongoDbConnector
    {
        public static IMongoDatabase Connect(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            return client.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
