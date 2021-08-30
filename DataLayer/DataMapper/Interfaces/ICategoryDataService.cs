// <copyright file="ICategoryDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the CategoryDataService.
    /// </summary>
    public interface ICategoryDataService : IRepository<Category>
    {
        /// <summary>
        /// Gets the number of opened auctions by category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A dictionary with key being the category and the value being the number of opened auctions.</returns>
        public Dictionary<long, int> GetNumberOfOpenedAuctionsByCategory(string userId);
    }
}
