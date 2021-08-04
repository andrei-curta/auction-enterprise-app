// <copyright file="IAuctionDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the AuctionDataService.
    /// </summary>
    public interface IAuctionDataService : IRepository<Auction>
    {
        /// <summary>
        /// Returns a list of auctions that the user has.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <returns>A list of auctions.</returns>
        public List<Auction> GetAuctionsByUser(User user);
    }
}