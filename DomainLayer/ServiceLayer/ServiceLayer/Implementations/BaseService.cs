// <copyright file="BaseService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataMapper.Repository;
    using FluentValidation;
    using Microsoft.Extensions.Logging;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Base service to provide common functionality.
    /// </summary>
    /// <typeparam name="TE">The Entity.</typeparam>
    /// <typeparam name="TS">The Service that corresponds to the Entity.</typeparam>
    /// <typeparam name="TV">The Validator that coresponds to the Entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class BaseService<TE, TS, TV> : IService<TE>
        where TV : AbstractValidator<TE>
        where TS : IRepository<TE>
    {
        /// <summary>
        /// The service that corresponds to the entity managed by this service.
        /// </summary>
        protected readonly TS service;

        /// <summary>
        /// The validator that corresponds to the entity managed by this service.
        /// </summary>
        protected readonly TV validator;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TE, TS, TV}"/> class.
        /// </summary>
        /// <param name="service">The service that corresponds to the entity managed by this service.</param>
        /// <param name="validator">The validator that corresponds to the entity managed by this service.</param>
        /// <param name="logger">The logger.</param>
        protected BaseService(TS service, TV validator, ILogger logger = null)
        {
            this.service = service;
            this.validator = validator;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public virtual void Add(TE entity)
        {
            try
            {
                this.validator.ValidateAndThrow(entity);

                this.service.Insert(entity);
            }
            catch (Exception e)
            {
                if (this.logger != null)
                {
                    this.logger.LogError(e, e.Message);
                }

                throw;
            }
        }

        /// <inheritdoc/>
        public virtual void Delete(TE entity)
        {
            try
            {
                this.service.Delete(entity);
            }
            catch (Exception e)
            {
                if (this.logger != null)
                {
                    this.logger.LogError(e, e.Message);
                }

                throw;
            }
        }

        // /// <inheritdoc/>
        // public virtual void Delete(object id)
        // {
        //     this.service.Delete(id);
        // }

        /// <inheritdoc/>
        public virtual TE GetById(long id)
        {
            try
            {
                return this.service.GetByID(id);
            }
            catch (Exception e)
            {
                if (this.logger != null)
                {
                    this.logger.LogError(e, e.Message);
                }

                throw;
            }
        }

        /// <inheritdoc/>
        public virtual IList<TE> List()
        {
            try
            {
                return this.service.Get().ToList();
            }
            catch (Exception e)
            {
                if (this.logger != null)
                {
                    this.logger.LogError(e, e.Message);
                }

                throw;
            }
        }

        /// <inheritdoc/>
        public virtual void Update(TE entity)
        {
            try
            {
                this.service.Update(entity);
            }
            catch (Exception e)
            {
                if (this.logger != null)
                {
                    this.logger.LogError(e, e.Message);
                }

                throw;
            }
        }
    }
}