using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Configuration
{
    public interface IDatabaseSettings
    {
        string MongoDbConnectionString { get; }
    }
}
