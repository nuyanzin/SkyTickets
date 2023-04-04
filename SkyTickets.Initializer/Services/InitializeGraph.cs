using SkyTickets.Domain.Entities;
using SkyTickets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Initializer.Services
{
    public class InitializeGraph
    {
        private readonly IGraphRepository _graphRepository;

        public InitializeGraph(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }

        public async Task Run()
        {
            var queryForLoadCountries =
                "LOAD CSV WITH HEADERS " +
                "FROM 'file:///countries.csv' " +
                "AS line " +
                "CREATE (:Country { code: line.Code, name: line.Name })";
            
            await _graphRepository.ExecuteQueryAsync(queryForLoadCountries);
        }

    }
}
