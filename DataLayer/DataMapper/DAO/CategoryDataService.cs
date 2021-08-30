// <copyright file="CategoryDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides data access functionality for <see cref="Category"/>.
    /// </summary>
    public class CategoryDataService : BaseRepository<Category>, ICategoryDataService
    {
        /// <inheritdoc/>
        public override Category GetByID(object id)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var category = ctx.Categories.Include(x => x.SubCategories).Include(x => x.ParentCategories)
                    .FirstOrDefault(x => x.Id == (long)id);

                return category;
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<Category> Get(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "")
        {
            includeProperties += ",SubCategories,ParentCategories";

            return base.Get(filter, orderBy, includeProperties);
        }

        /// <inheritdoc/>
        public virtual Dictionary<long, int> GetNumberOfOpenedAuctionsByCategory(string userId)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var userAuctions = ctx.Auctions.Include(x => x.Product).ThenInclude(x => x.Categories)
                    .Where(x => x.UserId == userId).Where(x => x.ClosedByOwner == false)
                    .Where(x => x.EndDate > DateTime.Now).ToList();

                var categories = new Dictionary<long, int>();

                foreach (var auction in userAuctions)
                {
                    foreach (var category in auction.Product.Categories)
                    {
                        int previousValue = 0;
                        if (categories.ContainsKey(category.Id))
                        {
                            previousValue = categories[category.Id];
                        }

                        categories[category.Id] = previousValue + 1;
                    }
                }

                return categories;
            }
        }
    }
}