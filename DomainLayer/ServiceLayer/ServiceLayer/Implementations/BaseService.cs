// <copyright file="BaseService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

namespace ServiceLayer.Implemantations
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Repository;
    using FluentValidation;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Base service to provide common functionality.
    /// </summary>
    /// <typeparam name="TE">The Entity.</typeparam>
    /// <typeparam name="TS">The Service that corresponds to the Entity.</typeparam>
    /// <typeparam name="TV">The Validator that coresponds to the Entity.</typeparam>
    public abstract class BaseService<TE, TS, TV> : IService<TE>
        where TV : AbstractValidator<TE>
        where TS : IRepository<TE>
    {
        public readonly TS service;
        private readonly TV validator;
        protected readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TE, TS, TV}"/> class.
        /// </summary>
        /// <param name="service">The service that corresponds to the entity managed by this service.</param>
        /// <param name="validator">The validator that corresponds to the entity managed by this service.</param>
        protected BaseService(TS service, TV validator, ILogger logger = null)
        {
            this.service = service;
            this.validator = validator;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public virtual void Add(TE entity)
        {
            this.validator.ValidateAndThrow(entity);

            this.service.Insert(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(TE entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public virtual TE GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public virtual IList<TE> List()
        {
            return service.Get().ToList();
        }

        /// <inheritdoc/>
        public virtual void Update(TE entity)
        {
            throw new System.NotImplementedException();
        }
    }
}