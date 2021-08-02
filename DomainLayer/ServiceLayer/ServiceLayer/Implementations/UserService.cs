// <copyright file="UserService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{

    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    public class UserService : BaseService<User, UserDataService, UserValidator>, IUserService
    {
        public UserService()
            : base(new UserDataService(), new UserValidator())
        {
        }
    }
}