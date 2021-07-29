// <copyright file="ApplicationSettingDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    public class ApplicationSettingDataService : BaseRepository<ApplicationSetting>, IApplicationSettingDataService
    {
    }
}