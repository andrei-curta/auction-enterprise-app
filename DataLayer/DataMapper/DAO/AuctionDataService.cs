// <copyright file="AuctionDataService.cs" company="Transilvania University of Brașov">
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
    /// Provides data access functionality for <see cref="Auction"/>.
    /// </summary>
    public class AuctionDataService : BaseRepository<Auction>, IAuctionDataService
    {
        /// <inheritdoc/>
        public virtual List<Auction> GetAuctionsByUserId(string userId)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Auctions.Where(x => x.UserId == userId).OrderByDescending(x => x.EndDate).ToList();
            }
        }
    }
}