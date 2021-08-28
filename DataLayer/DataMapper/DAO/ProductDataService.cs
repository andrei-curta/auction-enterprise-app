// <copyright file="ProductDataService.cs" company="Transilvania University of Brașov">
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
    /// Provides data access functionality for <see cref="Product"/>.
    /// </summary>
    public class ProductDataService : BaseRepository<Product>, IProductDataService
    {
        /// <inheritdoc/>
        public override Product GetByID(object id)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Products.Include(x => x.Categories).FirstOrDefault(x => x.Id == (long)id);
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<Product> Get(
            Expression<Func<Product, bool>> filter = null,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
            string includeProperties = "")
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<Product>().Include(x => x.Categories);

                IQueryable<Product> query = dbSet;

                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }
    }
}