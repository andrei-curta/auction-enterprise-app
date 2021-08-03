using System;
using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using ServiceLayer.Implementations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting up dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            var myService = serviceProvider.GetService<BidService>();

            // Call the service (logs are made here)
            myService.GetById(2);


        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
                {
                    config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                    config.AddConsole(); // Log to console (colored !)
                })
                .Configure<LoggerFilterOptions>(options =>
                {
                    options.AddFilter<DebugLoggerProvider>(null /* category*/, LogLevel.Information /* min level */);
                    options.AddFilter<ConsoleLoggerProvider>(null /* category*/, LogLevel.Warning /* min level */);
                });

            // Register service from the library
            services.AddTransient<BidService>();
        }
    }
}
