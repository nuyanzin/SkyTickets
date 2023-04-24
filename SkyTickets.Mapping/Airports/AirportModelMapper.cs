using SkyTickets.Domain.Entities;
using SkyTickets.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Mapping.Airports
{
    public class AirportModelMapper
    {
        public AirportModel Map(Airport entity)
            => entity == null ? throw new NullReferenceException() : new AirportModel()
            {
                Id = entity.Id,
                AirportId = entity.AirportId,
                Name = entity.Name,
                City = entity.City,
                Country = entity.Country,
                IATA = entity.IATA,
                ICAO = entity.ICAO,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Altitude = entity.Altitude,
                TimezoneInHours = entity.TimezoneInHours,
                DST = entity.DST,
                Timezone = entity.Timezone,
                Type = entity.Type,
                Source = entity.Source
            };
    }
}
