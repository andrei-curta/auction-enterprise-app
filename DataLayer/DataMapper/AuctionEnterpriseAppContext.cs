
namespace DataMapper
{
    using Microsoft.EntityFrameworkCore;
    using DomainModel.Models;

    class AuctionEnterpriseAppContext : DbContext
    {
        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }
    }
}
