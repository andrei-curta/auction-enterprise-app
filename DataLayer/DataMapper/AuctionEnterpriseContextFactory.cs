// <copyright file="AuctionEnterpriseContextFactory.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class AuctionEnterpriseContextFactory : IDesignTimeDbContextFactory<AuctionEnterpriseAppContext>
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public AuctionEnterpriseAppContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AuctionEnterpriseAppContext>();
            builder.UseSqlServer(this.Configuration.GetConnectionString("sqlServer"));

            return new AuctionEnterpriseAppContext(builder.Options);
        }
    }
}