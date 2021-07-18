
namespace DataMapper
{
    using Microsoft.EntityFrameworkCore;
    using DomainModel.Models;

    class AuctionEnterpriseAppContext : DbContext
    {

        public AuctionEnterpriseAppContext(DbContextOptions options) : base(options)
        {
            
        }
        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }
    }
}
