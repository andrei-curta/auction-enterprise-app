// <copyright file="ApplicationSettingImplementation.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using DataMapper.DAO;

namespace ServiceLayer.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using ServiceLayer.Interfaces;

    public class ApplicationSettingImplementation : IApplicationSettingService
    {
        private readonly IApplicationSettingDataService _applicationSettingDataService;

        public ApplicationSettingImplementation()
        {
            this._applicationSettingDataService = new ApplicationSettingDataService();
        }

        public void Add(ApplicationSetting entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ApplicationSetting entity)
        {
            throw new System.NotImplementedException();
        }

        public void GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public IList<ApplicationSetting> List()
        {
            return this._applicationSettingDataService.Get().ToList();
        }

        public void Update(ApplicationSetting entity)
        {
            throw new System.NotImplementedException();
        }
    }
}