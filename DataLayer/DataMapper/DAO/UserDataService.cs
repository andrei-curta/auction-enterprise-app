// <copyright file="UserDataService.cs" company="Transilvania University of Brașov">
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
    /// Provides data access functionality for <see cref="User"/>.
    /// </summary>
    public class UserDataService : BaseRepository<User>, IUserDataService
    {
        /// <inheritdoc/>
        public override IEnumerable<User> Get(Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "")
        {
            includeProperties += ",Roles";
            return base.Get(filter, orderBy, includeProperties);
        }

        /// <inheritdoc />
        public override User GetByID(object id)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Set<User>().Include(x => x.Roles).First(x => x.Id == (string)id);
            }
        }
    }
}