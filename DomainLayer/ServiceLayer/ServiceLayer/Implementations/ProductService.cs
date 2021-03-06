// <copyright file="ProductService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using FluentValidation;
    using ServiceLayer.Implementations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Product"/>.
    /// </summary>
    public class ProductService : BaseService<Product, ProductDataService, ProductValidator>, IProductService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productDataService">The data service.</param>
        public ProductService(ProductDataService productDataService)
            : base(productDataService, new ProductValidator())
        {
        }
    }
}