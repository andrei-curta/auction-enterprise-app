using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataMapper.DAO;
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

            // Get an instance of the Service
            // var myService = serviceProvider.GetService<AuctionService>();

            // Call the Service (logs are made here)
            // myService.List();

            // var appServ = serviceProvider.GetService<ApplicationSettingService>();
            //
            // var x = appServ.GetByName("test1");
            // Console.WriteLine(x.Value);

            var catServ = new CategoryService(new CategoryDataService());
            // catServ.Update(new Category()
            // {
            //     Id = 2,
            //     Name = "Chairs",
            //     SubCategories = new HashSet<Category>()
            //     {
            //         new Category()
            //         {
            //             Id = 1
            //         }
            //     }
            // });

           var a = catServ.List();

            // var prodServ = serviceProvider.GetService<ProductService>();


            var auctServ = new AuctionService(new AuctionDataService(), new ProductDataService(),
                new ApplicationSettingDataService());
            // auctServ.Add(new Auction()
            // {
            //     UserId = "1",
            //     ProductId = 1,
            //     StartDate = DateTime.Now.AddDays(1),
            //     EndDate = DateTime.Now.AddDays(20),
            //     StartPrice = new Money(10, "RON")
            // });

            var bidServ = new BidService(new BidDataService(), new AuctionDataService());
            // bidServ.Add(new Bid()
            // {
            //     AuctionId = 8,
            //     UserId = "1",
            //     BidValue = new Money(10.5M, "RON")
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

            // Register Service from the library
            services.AddTransient<ApplicationSettingService>();
            services.AddTransient<AuctionDataService>();
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