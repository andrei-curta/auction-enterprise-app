// <copyright file="CategoryValidator.cs" company="Transilvania University of Brașov">
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
    public class CategoryValidator : AbstractValidator<Category>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryValidator"/> class.
        /// </summary>
        public CategoryValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        }
    }
}
