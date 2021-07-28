// <copyright file="BaseService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>



namespace ServiceLayer.Implemantations
{
    using System.Collections.Generic;
    using FluentValidation;
    using ServiceLayer.Interfaces;
    using System.Linq;
    using DataMapper.Repository;

    /// <summary>
    /// Base service to provide common 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T, S, V> : IService<T>
        where V : AbstractValidator<T>
        where S : IRepository<T>
    {
        private readonly S service;
        private readonly V validator;

        protected BaseService(S service, V validator)
        {
            this.service = service;
            this.validator = validator;
        }

        public void Add(T entity)
        {
            this.validator.ValidateAndThrow(entity);

            this.service.Insert(entity);
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public IList<T> List()
        {
            return service.Get().ToList();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}