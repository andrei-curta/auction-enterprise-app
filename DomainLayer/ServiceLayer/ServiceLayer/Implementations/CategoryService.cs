﻿// <copyright file="CategoryService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    public class CategoryService : BaseService<Category, CategoryDataService, CategoryValidator>, ICategoryService
    {
        public CategoryService()
            : base(new CategoryDataService(), new CategoryValidator())
        {
        }
    }
}