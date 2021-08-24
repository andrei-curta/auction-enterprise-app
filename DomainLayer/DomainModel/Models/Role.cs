// <copyright file="Role.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The roles an user can have.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Role : IdentityRole
    {
        /// <summary>
        /// Gets or sets the users in this role.
        /// </summary>
        public virtual List<User> UsersInRole { get; set; }
    }
}