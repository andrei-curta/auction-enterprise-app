// <copyright file="IRepository.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for a generic repository.
    /// </summary>
    /// <typeparam name="T">The domain model that the repository corresponds to.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Inserts the entity into the database.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert(T entity);

        /// <summary>
        /// Updates the entity in the database.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        void Update(T item);

        /// <summary>
        /// Deletes the entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets an antyty by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity.</param>
        /// <returns>The entity that has a matching ID.</returns>
        T GetByID(object id);

        /// <summary>
        /// Gets the list of records from the database that match the criteria.
        /// </summary>
        /// <param name="filter">Expression to be used to filter the data.</param>
        /// <param name="orderBy">The property to order by.</param>
        /// <param name="includeProperties">The properties to be included, comma separated.</param>
        /// <returns>The list of objects that match the criteria.</returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}