using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.GraphService
{
    public interface IGraphService
    {
        Task<List<FlightPath>> GetPathsBetweenAirports(SimplePathQuery pathQuery);
    }
}
