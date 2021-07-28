// <copyright file="ApplicationSettingImplementation.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using DataMapper.DAO;
using DomainModel.Validators;
using ServiceLayer.Implemantations;

namespace ServiceLayer.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using ServiceLayer.Interfaces;

    public class ApplicationSettingImplementation : BaseService<ApplicationSetting, ApplicationSettingDataService, ApplicationSettingValidator>
    {
        public ApplicationSettingImplementation()
            : base(new ApplicationSettingDataService(), new ApplicationSettingValidator())
        {
        }
    }
}