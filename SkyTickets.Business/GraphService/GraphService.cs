using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Queries;
using SkyTickets.Domain.Repositories;

namespace SkyTickets.Business.GraphService
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;
        public GraphService(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }

        public Task<List<FlightPath>> GetPathsBetweenAirports(SimplePathQuery pathQuery)
        {
            return _graphRepository.GetPathsBetweenAirports(pathQuery);
        }
    }
}
