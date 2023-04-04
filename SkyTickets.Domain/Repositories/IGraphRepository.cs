using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Repositories
{
    public interface IGraphRepository : IRepository
    {
        Task ExecuteQueryAsync(string query);
    }
}
