using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.AirportsService
{
    public class AirportsService : IAirportsService
    {
        private readonly IAirportsRepository _airportsRepository;

        public AirportsService(IAirportsRepository airportsRepository)
        {
            _airportsRepository = airportsRepository;
        }

        public Task<List<Airport>> GetBySearchTermAsync(string searchTerm, int limit)
        {
            return _airportsRepository.GetBySearchTermAsync(searchTerm, limit); 
        }
    }
}
