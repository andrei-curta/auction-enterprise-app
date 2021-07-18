namespace DataMapper
{
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    class AuctionEnterpriseContextFactory : IDesignTimeDbContextFactory<AuctionEnterpriseAppContext>
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