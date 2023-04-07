using SkyTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Repositories
{
    public interface IAirportsRepository : IRepository<Airport>
    {
        Task<List<Airport>> GetBySearchTermAsync(string? searchTerm = null);
    }
}
