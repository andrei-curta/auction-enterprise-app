// <copyright file="UserService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implementations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="User"/>.
    /// </summary>
    public class UserService : BaseService<User, UserDataService, UserValidator>, IUserService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDataService"></param>
        public UserService(UserDataService userDataService)
            : base(userDataService, new UserValidator())
        {
        }
    }
}