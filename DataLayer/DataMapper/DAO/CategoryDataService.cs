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
        public override Category GetByID(object id)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var category = ctx.Categories.Include(x => x.SubCategories).Include(x => x.ParentCategories)
                    .FirstOrDefault(x => x.Id == (long) id);

                return category;
            }
        }

        public override IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<Category>().Include(x=> x.ParentCategories).Include(x=> x.SubCategories);

                IQueryable<Category> query = dbSet;

                foreach (var includeProperty in includeProperties.Split(
                    new char[] {','},
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