// <copyright file="BidValidator.cs" company="Transilvania University of Brașov">
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
    public class BidValidator : AbstractValidator<Bid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BidValidator"/> class.
        /// </summary>
        public BidValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
