﻿// <copyright file="AuctionEnterpriseAppContext.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper
{
    using DomainModel.Models;
    using Microsoft.EntityFrameworkCore;

    public class AuctionEnterpriseAppContext : DbContext
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

            modelBuilder.Entity<Auction>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");
        }

        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}