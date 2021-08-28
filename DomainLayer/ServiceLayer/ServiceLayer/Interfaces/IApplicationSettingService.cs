// <copyright file="IApplicationSettingService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>
    /// Interface for the Application settings service.
    /// </summary>
    public interface IApplicationSettingService : IService<ApplicationSetting>
    {
        /// <summary>
        /// Gets an application setting by its name..
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ApplicationSetting.</returns>
        public ApplicationSetting GetByName(string name);

        /// <summary>
        /// Returns the value of the setting as a string if it exists.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting as string.</returns>
        public string GetValueAsString(string name);

        /// <summary>
        /// Returns the value of the setting as an int if it exists.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting as int.</returns>
        public int GetValueAsInt(string name);

        /// <summary>
        /// Returns the value of the setting as a bool if it exists.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting as bool.</returns>
        public bool GetValueAsBool(string name);

        /// <summary>
        /// Returns the value of the setting as a decimal if it exists.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting as decimal.</returns>
        public decimal GetValueAsDecimal(string name);

        /// <summary>
        /// Gets the value as double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Double.</returns>
        public double GetValueAsDouble(string name);
    }
}