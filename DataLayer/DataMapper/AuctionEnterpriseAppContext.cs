// <copyright file="AuctionEnterpriseAppContext.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper
{
    using DomainModel.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class AuctionEnterpriseAppContext.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AuctionEnterpriseAppContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionEnterpriseAppContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AuctionEnterpriseAppContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        /// <value>The application settings.</value>
        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        /// <summary>
        /// Gets or sets the auctions.
        /// </summary>
        /// <value>The auctions.</value>
        public virtual DbSet<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the bids.
        /// </summary>
        /// <value>The bids.</value>
        public virtual DbSet<Bid> Bids { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the scores.
        /// </summary>
        /// <value>The scores.</value>
        public virtual DbSet<Score> Scores { get; set; }

        /// <summary>
        /// Gets or sets the auction placing restrictions.
        /// </summary>
        /// <value>The auction placing restrictions.</value>
        public virtual DbSet<AuctionPlacingRestriction> AuctionPlacingRestrictions { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specifies the monet property is a value type and not an entity, such no other table is created and the properties are defined as columns in the product table.
            modelBuilder.Entity<Auction>().OwnsOne(x => x.StartPrice);
            modelBuilder.Entity<Bid>().OwnsOne(x => x.BidValue);

            modelBuilder.Entity<Bid>().Property(b => b.DateAdded).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Auction>().Property(b => b.DateCreated).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Score>().Property(b => b.DateAdded).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Auction>().Property(b => b.ClosedByOwner).HasDefaultValue(false);
            modelBuilder.Entity<Auction>().HasOne(c => c.Product).WithMany().OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Auction>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");

            modelBuilder.Entity<Bid>()
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("UserId");
        }
    }
}