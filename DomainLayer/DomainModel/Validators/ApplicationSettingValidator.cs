// <copyright file="ApplicationSettingValidator.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Validators
{
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;
    using FluentValidation;

    /// <summary>
    /// Validator for <see cref="ApplicationSetting"/>.
    /// </summary>
    public class ApplicationSettingValidator : AbstractValidator<ApplicationSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingValidator"/> class.
        /// </summary>
        public ApplicationSettingValidator()
        {
            this.RuleFor(x => x.Name).MinimumLength(3);

            this.RuleFor(x => x.Name).NotEmpty().Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Only letters, numbers and - _ are allowed");

            this.RuleFor(x => x.Value).NotEmpty();
        }
    }
}