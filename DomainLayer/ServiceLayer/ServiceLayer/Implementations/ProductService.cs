// <copyright file="ProductService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    public class ProductService : BaseService<Product, ProductDataService, ProductValidator>, IProductService
    {
        public ProductService()
            : base(new ProductDataService(), new ProductValidator())
        {
        }
    }
}