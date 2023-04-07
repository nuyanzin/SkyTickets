using SkyTickets.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.FlightStatsSevice
{
    public class FlightStatsService : IFlightStatsService
    {
        private readonly IFlightStatsApiSettings _flightStatsApiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public FlightStatsService(
            IFlightStatsApiSettings flightStatsApiSettings,
            IHttpClientFactory httpClientFactory
            )
        {
            _flightStatsApiSettings = flightStatsApiSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task GetScheduledFlightsByRouteAsync(FlightStatsQuery query)
        {
            var requestUri = "https://api.flightstats.com/flex/schedules/rest/v1/json/";
            requestUri += $"from/{query.DepartureAirportCode}/";
            requestUri += $"to/{query.ArrivalAirportCode}/";
            requestUri += $"departing/{query.DateOfDeparture.Year}/{query.DateOfDeparture.Month}/{query.DateOfDeparture.Day}";

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
            };
            request.Headers.Add("appId", _flightStatsApiSettings.AppId);
            request.Headers.Add("appKey", _flightStatsApiSettings.AppKey);

            using var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

            var str = await response.Content.ReadAsStringAsync();
        }
    }

    public class FlightStatsQuery 
    {
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }
        public DateTime DateOfDeparture { get; set; }

        public FlightStatsQuery(string departureAirportCode, string arrivalAirportCode, DateTime dateOfDeparture)
        {
            DepartureAirportCode = departureAirportCode;
            ArrivalAirportCode = arrivalAirportCode;
            DateOfDeparture = dateOfDeparture;
        }
    }
}
