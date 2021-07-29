// <copyright file="UserDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Provides data access functionality for <see cref="User"/>.
    /// </summary>
    public class UserDataService : BaseRepository<User>, IUserDataService
    {
    }
}