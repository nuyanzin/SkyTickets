using Microsoft.AspNetCore.Mvc;
using SkyTickets.Business.AirportsService;
using SkyTickets.Mapping.Airports;
using SkyTickets.Model.Models;

namespace SkyTickets.WebApi.Controllers
{
    [Route("airports")]
    public class AirportsApiController : Controller
    {
        private readonly IAirportsService _airportsService;
        private readonly AirportModelMapper _mapper;
        public AirportsApiController(
            IAirportsService airportsService,
            AirportModelMapper mapper)
        {
            _airportsService = airportsService;
            _mapper = mapper;
        }

        [HttpGet, Route("by-search-term")]
        public async Task<List<AirportModel>> GetBySearchTerm(string searchTerm = "", int limit = 5)
        {
            var airports = await _airportsService.GetBySearchTermAsync(searchTerm, limit);
            return airports.Select(_mapper.Map).ToList();
        }

    }
}
