using Microsoft.AspNetCore.Mvc;
using SkyTickets.Business.GraphService;
using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Queries;

namespace SkyTickets.WebApi.Controllers
{
    [Route("graph")]
    public class GraphApiController : Controller
    {
        private readonly IGraphService _graphService;

        public GraphApiController(IGraphService graphService)
        {
            _graphService = graphService;
        }

        [HttpPost, Route("paths-between-airports")]
        public Task<List<FlightPath>> GetPathsBetweenAirports([FromBody] SimplePathQuery pathQuery)
        {
            return _graphService.GetPathsBetweenAirports(pathQuery);
        }
    }
}
