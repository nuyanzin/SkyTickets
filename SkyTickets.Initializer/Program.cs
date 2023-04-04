using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyTickets.Initializer.Services;
using System.Reflection;

namespace SkyTickets.Initializer
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Initializer started.");
            Console.BackgroundColor = ConsoleColor.White;
            //var parsedArgs = Args.Parse(args);
            //var environment = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyEnvironmentAttribute>()?.Environment;
            _configuration = Startup.BuildConfiguration("Development");
            _serviceProvider = Startup.BuildServiceProvider(_configuration);

            await _serviceProvider.GetService<InitializeGraph>().Run();
        }
    }
}