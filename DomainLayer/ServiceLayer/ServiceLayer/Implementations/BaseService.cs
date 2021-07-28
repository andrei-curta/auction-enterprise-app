// <copyright file="BaseService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using System.Linq;
using DataMapper.Repository;

namespace ServiceLayer.Implemantations
{
    using System.Collections.Generic;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Base service to provide common 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T, S> : IService<T>
        where S : IRepository<T>
    {
        private readonly S service;

        protected BaseService(S service)
        {
            this.service = service;
        }

        public void Add(T entity)
        {
            service.Insert(entity);
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