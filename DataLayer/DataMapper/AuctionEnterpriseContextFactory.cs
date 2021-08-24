// <copyright file="AuctionEnterpriseContextFactory.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Factory class that creates EF contexts.
    /// </summary>
    public class AuctionEnterpriseContextFactory : IDesignTimeDbContextFactory<AuctionEnterpriseAppContext>
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// Creates a db context based on the arguments.
        /// </summary>
        /// <param name="args">Aditional arguments used for creating the context.</param>
        /// <returns>An EF context.</returns>
        public AuctionEnterpriseAppContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AuctionEnterpriseAppContext>();
            builder.UseSqlServer(this.Configuration.GetConnectionString("sqlServer"));

            return new AuctionEnterpriseAppContext(builder.Options);
        }
    }
}