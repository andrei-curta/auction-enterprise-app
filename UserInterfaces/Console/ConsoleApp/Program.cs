using System;
using System.Diagnostics.CodeAnalysis;
using DomainModel.Models;
using DomainModel.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Serilog;
using ServiceLayer.Implementations;

namespace ConsoleApp
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            // Setting up dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            var myService = serviceProvider.GetService<AuctionService>();

            // Call the service (logs are made here)
            // myService.List();

            // var appServ = serviceProvider.GetService<ApplicationSettingService>();
            //
            // var x = appServ.GetByName("test1");
            // Console.WriteLine(x.Value);

            var catServ = serviceProvider.GetService<CategoryService>();
            // catServ.Add(new Category(){Name = "Furniture"});

            var prodServ = serviceProvider.GetService<ProductService>();

            var prod = new Product()
            {
                Description = "another chair",
                Name = "chair2",

                UserId = "1",
                // Value = new Money(10, "RON")
            };

            prodServ.Add(prod);

            var auctServ = serviceProvider.GetService<AuctionService>();
            // auctServ.Add(new Auction()
            // {
            //     UserId = "1",
            //     ProductId = 1,
            //     StartDate = DateTime.Now.AddDays(1),
            //     EndDate = DateTime.Now.AddDays(20)
            // });

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();


            services.AddLogging(configure => configure.AddSerilog());

            // Register service from the library
            services.AddTransient<ApplicationSettingService>();
            services.AddTransient<AuctionService>();
            services.AddTransient<BidService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<UserService>();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(); // <- Add this line
    }
}