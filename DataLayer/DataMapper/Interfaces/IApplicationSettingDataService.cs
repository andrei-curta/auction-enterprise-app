// <copyright file="IApplicationSettingDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the ApplicationSettingsDataService.
    /// </summary>
    public interface IApplicationSettingDataService : IRepository<ApplicationSetting>
    {
        /// <summary>
        /// Gets an applicationSetting by its name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The corresponding application setting.</returns>
        public ApplicationSetting GetByName(string name);
    }
}
