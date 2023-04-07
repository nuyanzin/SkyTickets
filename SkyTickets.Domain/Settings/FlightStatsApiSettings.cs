using SkyTickets.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Settings
{
    public class FlightStatsApiSettings : IFlightStatsApiSettings
    {
        public string AppId { get; set; }

        public string AppKey { get; set; }
    }
}
