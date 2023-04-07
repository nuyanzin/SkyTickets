using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Configuration
{
    public interface IFlightStatsApiSettings
    {
        string AppId { get; }
        string AppKey { get; }
    }
}
