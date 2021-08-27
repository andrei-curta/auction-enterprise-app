// <copyright file="AuctionPlacingRestrictionsDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using System;
    using System.Linq;
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Implementation of the auction placing data service.
    /// </summary>
    public class AuctionPlacingRestrictionsDataService : BaseRepository<AuctionPlacingRestriction>,
        IAuctionPlacingRestrictionsDataService
    {
        /// <inheritdoc/>
        public bool HasActiveAuctionPlacingRestrictions(string userId)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.AuctionPlacingRestrictions.Where(x => x.User.Id == userId)
                    .Any(x => x.EndDate > DateTime.Now);
            }
        }
    }
}