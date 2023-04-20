using SkyTickets.Domain.Settings;
using SkyTickets.Data.Neo4j;
using Neo4j.Driver;
using SkyTickets.Data.Repositories;
using SkyTickets.Domain.Repositories;
using SkyTickets.Business.GraphService;
using SkyTickets.Domain.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyTickets.Business.FlightStatsSevice;
using SkyTickets.Business.AirportsService;
using MongoDB.Driver;
using SkyTickets.Data;
using SkyTickets.Mapping.Airports;

namespace SkyTickets.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection(typeof(Neo4jSettings).Name).Get<Neo4jSettings>();
            var mongoDbSettings = Configuration.GetSection(typeof(DatabaseSettings).Name).Get<DatabaseSettings>();
            var settings1 = Configuration.GetSection(typeof(FlightStatsApiSettings).Name).Get<FlightStatsApiSettings>();
            services
                .AddSingleton<INeo4jSettings>(settings)
                .AddSingleton<IDatabaseSettings>(mongoDbSettings)
                .AddSingleton<IFlightStatsApiSettings>(settings1);

            services
                .AddSingleton(typeof(IDriver), Neo4jContext.Connect(settings))
                .AddSingleton(typeof(IMongoDatabase), MongoDbConnector.Connect(mongoDbSettings.MongoDbConnectionString));

            services
                .AddSingleton<IDatabaseQueryExecutor, Neo4jDatabaseQueryExecutor>();

            services
                .AddSingleton<IGraphRepository, Neo4jRepository>()
                .AddSingleton<IAirportsRepository, AirportsRepository>();

            services
                .AddSingleton<IGraphService, GraphService>()
                .AddSingleton<IFlightStatsService, FlightStatsService>()
                .AddSingleton<IAirportsService, AirportsService>();

            services
                .AddSingleton<AirportModelMapper>();

            services
                .AddHttpClient();

            services
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
