using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyTickets.Data.Neo4j;
using SkyTickets.Data.Repositories;
using SkyTickets.Domain.Repositories;
using SkyTickets.Domain.Settings;
using SkyTickets.Initializer.Services;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyTickets.Business.GraphService;

namespace SkyTickets.Initializer
{
    public enum CurrentEnvironment
    {
        Local, Staging, Production
    }

    public class Startup
    {
        public static CurrentEnvironment Env { get; private set; } = CurrentEnvironment.Local;

        public static IConfigurationRoot BuildConfiguration(string environment)
        {
            Env = environment?.ToLower() switch
            {
                "staging" => CurrentEnvironment.Staging,
                "release" => CurrentEnvironment.Production,
                _ => CurrentEnvironment.Local
            };

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static IServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();

            var settings = configuration.GetSection(typeof(Neo4jSettings).Name).Get<Neo4jSettings>();
            
            services
                .AddSingleton<Neo4jSettings>(settings);

            services
                .AddSingleton(typeof(IDriver), Neo4jContext.Connect(settings));

            services
                .AddSingleton<IDatabaseQueryExecutor, Neo4jDatabaseQueryExecutor>();

            services
                .AddSingleton<IGraphRepository, Neo4jRepository>();

            services
                .AddSingleton<IGraphService, GraphService>();

            services
                .AddSingleton<InitializeGraph>();

            return services.BuildServiceProvider();
        }
    }
}
