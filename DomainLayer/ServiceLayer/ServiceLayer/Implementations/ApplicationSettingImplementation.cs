// <copyright file="ApplicationSettingImplementation.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using DataMapper.DAO;
using ServiceLayer.Implemantations;

namespace ServiceLayer.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using ServiceLayer.Interfaces;

    public class ApplicationSettingImplementation : BaseService<ApplicationSetting, ApplicationSettingDataService>
    {
        public ApplicationSettingImplementation() : base(new ApplicationSettingDataService())
        {
        }
    }
}