using MongoDB.Driver;
using Neo4j.Driver;
using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Repositories;

namespace SkyTickets.Data.Repositories
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected virtual IMongoCollection<TEntity> Collection { get; private set; }

        protected MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        public Task<TEntity> GetAsync(Guid id)
        {
            return Collection.Find(x => x.Id == id).SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> GetAsync(params Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return Task.FromResult(new List<TEntity>());
            }

            return Collection.Find(x => ids.Contains(x.Id)).ToListAsync();
        }

        public Task CreateAsync(TEntity entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public Task CreateBatchAsync(params TEntity[] entities)
        {
            var list = entities.ToList();
            if (list.Count == 0)
            {
                return Task.FromResult(false);
            }

            return Collection.InsertManyAsync(list);
        }

        public Task UpdateAsync(TEntity entity)
        {
            return Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);

        }

        public Task DeleteAsync(Guid id)
        {
            return Collection.DeleteManyAsync(x => x.Id == id);
        }
    }
}
