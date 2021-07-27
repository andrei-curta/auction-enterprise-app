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
        public virtual DbSet<Auction> Auctions{ get; set; }
        public virtual DbSet<Bid> Bids{ get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Product> Products{ get; set; }
        public virtual DbSet<User> Users{ get; set; }
        
        
    }
}