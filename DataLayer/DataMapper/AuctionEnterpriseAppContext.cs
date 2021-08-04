using System;
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
            // Specifies the monet property is a value type and not an entity, such no other table is created and the properties are defined as columns in the product table.
            modelBuilder.Entity<Product>().OwnsOne(x => x.Value);
            modelBuilder.Entity<Bid>().OwnsOne(x => x.BidValue);

            modelBuilder.Entity<Bid>().Property(b => b.DateAdded).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Auction>().Property(b => b.DateCreated).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Auction>().Property(b => b.ClosedByOwner).HasDefaultValue(false);
        }

        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}