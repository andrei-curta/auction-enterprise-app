// <copyright file="ApplicationSettingService.cs" company="Transilvania University of Brașov">
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
    /// Provides services regarding the <see cref="ApplicationSetting"/>.
    /// </summary>
    public class ApplicationSettingService : BaseService<ApplicationSetting, ApplicationSettingDataService,
        ApplicationSettingValidator>, IApplicationSettingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingService"/> class.
        /// </summary>
        public ApplicationSettingService(ApplicationSettingDataService applicationSettingDataService)
            : base(applicationSettingDataService, new ApplicationSettingValidator())
        {
        }

        /// <inheritdoc/>
        public ApplicationSetting GetByName(string name)
        {
            return this.service.GetByName(name);
        }

        /// <inheritdoc/>
        public string GetValueAsString(string name)
        {
            return this.service.GetByName(name).Value;
        }

        /// <inheritdoc/>
        public int GetValueAsInt(string name)
        {
            return int.Parse(this.service.GetByName(name).Value);
        }

        /// <inheritdoc/>
        public bool GetValueAsBool(string name)
        {
            return bool.Parse(this.service.GetByName(name).Value);
        }

        /// <inheritdoc/>
        public decimal GetValueAsDecimal(string name)
        {
            return decimal.Parse(this.service.GetByName(name).Value);
        }
    }
}