using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.FlightStatsSevice
{
    public interface IFlightStatsService
    {
        Task GetScheduledFlightsByRouteAsync(FlightStatsQuery query);
    }
}
