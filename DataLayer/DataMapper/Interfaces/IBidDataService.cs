// <copyright file="IBidDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the BidDataService.
    /// </summary>
    public interface IBidDataService : IRepository<Bid>
    {
        /// <summary>
        /// Gets a list of bids that were placed on the auction.
        /// </summary>
        /// <param name="auctionId">The auction to search bids for.</param>
        /// <returns>The list of corresponding bids.</returns>
        public List<Bid> GetBidsByAuction(long auctionId);

        /// <summary>
        /// Gets the latest bid that was placed on the auction.
        /// </summary>
        /// <param name="auctionId">The auction to search bids for.</param>
        /// <returns>The latest bid or null.</returns>
        public Bid GetLatestBidByAuction(long auctionId);
    }
}