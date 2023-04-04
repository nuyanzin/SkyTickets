using Microsoft.AspNetCore.Mvc;
using SkyTickets.Business.GraphService;

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

        [HttpPost, Route("create")]
        public async Task CreateCountries()
        {
        }
    }
}
