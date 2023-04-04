using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Configuration
{
    public interface INeo4jSettings
    {
        string DatabaseUri { get; }
        string UserName { get; }
        string Password { get; }
    }
}
