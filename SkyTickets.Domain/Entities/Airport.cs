using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Entities
{
    public class Airport : Entity
    {
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

        public void InitializePropertiesFromString(string dataRow)
        {
            var fields = dataRow.Split(',');
            if (int.TryParse(fields[0], out var id))
            {
                AirportId = id;
            }
            Name = ParseStringDataFromField(fields[1]);
            Country = ParseStringDataFromField(fields[2]);
            IATA = ParseStringDataFromField(fields[3]);
            ICAO = ParseStringDataFromField(fields[4]);
            Latitude = ParseDoubleDataFromField(fields[5]);
            Longitude = ParseDoubleDataFromField(fields[6]);
            Altitude = ParseDoubleDataFromField(fields[7]);
            TimezoneInHours = ParseIntDataFromField(fields[8]);
            DST = ParseStringDataFromField(fields[9]);
            Timezone = ParseStringDataFromField(fields[10]);
            Type = ParseStringDataFromField(fields[11]);
            Source = ParseStringDataFromField(fields[12]);
        }

        private string? ParseStringDataFromField(string field)
        {
            var data = field.Split("\"")[1];
            if (!string.IsNullOrEmpty(data))
            {
                if (data != "Null")
                {
                    return data;
                }
                return null;
            }
            return null;
        }

        private double? ParseDoubleDataFromField(string field)
        {
            if (double.TryParse(field, out double number))
            {
                return number;
            }
            return null;
        }

        private int? ParseIntDataFromField(string field)
        {
            if (int.TryParse(field, out int number))
            {
                return number;
            }
            return null;
        }
    }
}
