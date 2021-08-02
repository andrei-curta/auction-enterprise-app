// <copyright file="ApplicationSettingImplementation.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    public class ApplicationSettingService : BaseService<ApplicationSetting, ApplicationSettingDataService,
        ApplicationSettingValidator>, IApplicationSettingService
    {
        public ApplicationSettingService()
            : base(new ApplicationSettingDataService(), new ApplicationSettingValidator())
        {
        }
    }
}