// <copyright file="BaseService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implemantations
{
    using System.Collections.Generic;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Base service to provide common 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IService<T>
    {


        public void Add(T entity)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}