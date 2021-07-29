// <copyright file="IProductDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the ProductDataService.
    /// </summary>
    public interface IProductDataService : IRepository<Product>
    {
    }
}
