// <copyright file="User.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The application user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the products that a user has.
        /// </summary>
        public List<Product> Products { get; set; }
    }
}