using SkyTickets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Business.GraphService
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;
        public GraphService(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }
    }
}
