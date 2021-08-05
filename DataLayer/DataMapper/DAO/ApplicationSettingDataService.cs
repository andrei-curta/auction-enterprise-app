// <copyright file="ApplicationSettingDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using System.Linq;

namespace DataMapper.DAO
{
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Provides data access functionality for <see cref="ApplicationSetting"/>.
    /// </summary>
    public class ApplicationSettingDataService : BaseRepository<ApplicationSetting>, IApplicationSettingDataService
    {

        /// <inheritdoc/>
        public ApplicationSetting GetByName(string name)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                return ctx.ApplicationSettings.First(x => x.Name == name);
            }
        }
    }
}