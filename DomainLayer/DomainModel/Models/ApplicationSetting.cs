// <copyright file="ApplicationSetting.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Manages settings used in the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApplicationSetting
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the setting.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the value of the setting.
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}