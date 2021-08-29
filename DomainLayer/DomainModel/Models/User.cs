// <copyright file="User.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using System.Linq;

namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the roles the user has.
        /// </summary>
        public virtual List<Role> Roles { get; set; }

        /// <summary>
        /// Determines whether the user is in the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns><c>true</c> if [is in role] [the specified role name]; otherwise, <c>false</c>.</returns>
        public bool IsInRole(string roleName)
        {
            return this.Roles.Any(x => x.NormalizedName == roleName.ToUpper().Trim());
        }
    }
}