// <copyright file="IService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface to be implemented by all services. Provides basic crud functionality.
    /// </summary>
    /// <typeparam name="T">The entity that this service works with.</typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        void Add(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Retrieves a list with all entities.
        /// </summary>
        /// <returns>All the entities of this type.</returns>
        IList<T> List();

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The updated entity.</param>
        void Update(T entity);

        /// <summary>
        /// Gets an entity by its Id.
        /// </summary>
        /// <param name="id">Id of the entity to be retrieved.</param>
        /// <returns>The entity that has a corresponding id to <paramref name="id"/>.</returns>
        T GetById(long id);
    }
}