using SkyTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        Task<TEntity> GetAsync(Guid id);
        Task<List<TEntity>> GetAsync(params Guid[] ids);
        Task CreateAsync(TEntity entity);
        Task CreateBatchAsync(params TEntity[] entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
