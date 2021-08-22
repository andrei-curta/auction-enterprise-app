// <copyright file="BidDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Provides data access functionality for <see cref="Category"/>.
    /// </summary>
    public class BidDataService : BaseRepository<Bid>, IBidDataService
    {
        /// <inheritdoc/>
        public List<Bid> GetBidsByAuction(long auctionId)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Bids.Where(bid => bid.AuctionId == auctionId).OrderByDescending(bid => bid.DateAdded)
                    .ToList();
            }
        }

        /// <inheritdoc/>
        public Bid GetLatestBidByAuction(long auctionId)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Bids.Where(bid => bid.AuctionId == auctionId).OrderByDescending(bid => bid.DateAdded)
                    .FirstOrDefault();
            }
        }
    }
}