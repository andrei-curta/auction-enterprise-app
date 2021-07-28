using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMapper
{
    using Microsoft.EntityFrameworkCore;
    using DomainModel.Models;

    class AuctionEnterpriseAppContext : DbContext
    {
        public AuctionEnterpriseAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specifies the monet property is a value type and not an entyty, such no other table is created and the properties are defined as columns in the product table.
            modelBuilder.Entity<Product>().OwnsOne(x => x.Value);

        }

        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}