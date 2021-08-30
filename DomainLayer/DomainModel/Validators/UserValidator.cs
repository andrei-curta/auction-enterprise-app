// <copyright file="UserValidator.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Validators
{
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;
    using FluentValidation;

    /// <summary>
    /// Validator for <see cref="Auction"/>.
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            this.RuleFor(x => x.UserName).NotEmpty().MinimumLength(5);

            this.RuleFor(x => x.Email).EmailAddress();

            this.RuleFor(x => x.PhoneNumber).Matches("[0-9]{10}");
        }
    }
}
