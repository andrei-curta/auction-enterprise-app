// <copyright file="BaseRepository.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides basic data access functionality for entity.
    /// </summary>
    /// <typeparam name="T">The entity it offers data access for.</typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        // private readonly AuctionEnterpriseAppContext _context;
        //
        // /// <summary>
        // /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        // /// </summary>
        // /// <param name="context">The EF context.</param>
        // protected BaseRepository(AuctionEnterpriseAppContext context)
        // {
        //     this._context = context;
        // }

        /// <inheritdoc/>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<T>().AsQueryable();

                IQueryable<T> query = dbSet;

                // remove empty spaces that can cause errors
                includeProperties = includeProperties.Replace(" ", string.Empty);

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

        /// <inheritdoc/>
        public virtual void Insert(T entity)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<T>();
                dbSet.Add(entity);

                ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public virtual void Update(T item)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(item);
                ctx.Entry(item).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an record from the database based on its id.
        /// </summary>
        /// <param name="id">The id of teh record.</param>
        public virtual void Delete(object id)
        {
            this.Delete(this.GetByID(id));
        }

        /// <inheritdoc/>
        public virtual void Delete(T entityToDelete)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var dbSet = ctx.Set<T>();

                if (ctx.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);

                ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public virtual T GetByID(object id)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.Set<T>().Find(id);
            }
        }
    }
}