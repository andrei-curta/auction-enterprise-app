// <copyright file="IService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;

    public interface IService<T>
    {
        IList<T> List();

        void Delete(T entity);

        void Update(T entity);

        void GetById(long id);

        void Add(T entity);
    }
}