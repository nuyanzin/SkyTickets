using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Model.Models
{
    public class AirportModel
    {
        public Guid Id { get; set; }
        public int AirportId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? IATA { get; set; }
        public string? ICAO { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public int? TimezoneInHours { get; set; }
        public string? DST { get; set; }
        public string? Timezone { get; set; }
        public string? Type { get; set; }
        public string? Source { get; set; }
    }
}
