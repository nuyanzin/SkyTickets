using MongoDB.Driver;
using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkyTickets.Data.Repositories
{
    public class AirportsRepository : MongoRepository<Airport>, IAirportsRepository
    {
        public const string CollectionName = "airports";

        public AirportsRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName) { }

        public async Task<List<Airport>> GetBySearchTermAsync(string? searchTerm = null, int? limit = null)
        {
            if (searchTerm == null)
            {
                return new List<Airport> { };
            }

            var builder = Builders<Airport>.Filter;
            var regex = new Regex(Regex.Escape(searchTerm));
            var filter = builder.Regex(x => x.Name, regex)
                | builder.Regex(x => x.City, regex)
                | builder.Regex(x => x.IATA, regex)
                | builder.Regex(x => x.ICAO, regex);
            return await Collection.Find(filter).Limit(limit).ToListAsync();
        }
    }
}
