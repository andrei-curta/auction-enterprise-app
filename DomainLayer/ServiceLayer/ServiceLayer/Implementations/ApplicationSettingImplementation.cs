// <copyright file="ApplicationSettingImplementation.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;

    public class ApplicationSettingImplementation : BaseService<ApplicationSetting, ApplicationSettingDataService,
        ApplicationSettingValidator>
    {
        public ApplicationSettingImplementation()
            : base(new ApplicationSettingDataService(), new ApplicationSettingValidator())
        {
        }
    }
}