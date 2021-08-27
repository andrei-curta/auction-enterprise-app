// <copyright file="IAuctionPlacingRestrictionsDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the auctionPlacingRestrictionsDataService.
    /// </summary>
    public interface IAuctionPlacingRestrictionsDataService : IRepository<AuctionPlacingRestriction>
    {
        /// <summary>
        /// Check if the user can start auctions or not.
        /// </summary>
        /// <param name="userId">The Id of the user to find out if it is unser restrictions.</param>
        /// <returns>A boolean representing if the user is under restrictions.</returns>
        public bool HasActiveAuctionPlacingRestrictions(string userId);
    }
}
