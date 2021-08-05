// <copyright file="ApplicationSettingService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="ApplicationSetting"/>.
    /// </summary>
    public class ApplicationSettingService : BaseService<ApplicationSetting, ApplicationSettingDataService,
        ApplicationSettingValidator>, IApplicationSettingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingService"/> class.
        /// </summary>
        public ApplicationSettingService()
            : base(new ApplicationSettingDataService(), new ApplicationSettingValidator())
        {
        }

        public ApplicationSetting GetByName(string name)
        {
            return this.GetByName(name);
        }
    }
}