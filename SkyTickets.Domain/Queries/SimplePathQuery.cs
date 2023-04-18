using SkyTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkyTickets.Domain.Queries
{
    public class SimplePathQuery
    {
        public SimpleFlight? Flight { get; set; }
        public int NumberOfTransfers { get; set; } = 1;
    }

    public class SimpleFlight {
        public Guid DepartureAirportEntityId { get; set;}

        public Guid ArrivalAirportEntityId { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }
    }
}
