using SkyTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.AirportsService
{
    public interface IAirportsService
    {
        Task<List<Airport>> GetBySearchTermAsync(string searchTerm, int limit);
    }
}
