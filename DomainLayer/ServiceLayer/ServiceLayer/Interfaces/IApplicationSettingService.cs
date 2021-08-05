// <copyright file="IApplicationSettingService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    public interface IApplicationSettingService : IService<ApplicationSetting>
    {
        public ApplicationSetting GetByName(string name);

    }
}