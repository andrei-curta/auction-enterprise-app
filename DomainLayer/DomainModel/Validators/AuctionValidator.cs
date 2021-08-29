// <copyright file="AuctionValidator.cs" company="Transilvania University of Brașov">
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
    public class AuctionValidator : AbstractValidator<Auction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionValidator"/> class.
        /// </summary>
        public AuctionValidator()
        {
            this.RuleFor(x => x.StartDate).NotEmpty().LessThan(x => x.EndDate).WithMessage("Start date cannot be after End date");
            this.RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date must be specified.");

            this.RuleFor(x => x.ProductId).NotEmpty();

            this.RuleFor(x => x.StartPrice).NotNull();
            this.RuleFor(x => x.StartPrice.Amount).GreaterThan(0).When(x => x.StartPrice != null);
            this.RuleFor(x => x.StartPrice.Currency).Length(3).Matches("[A-Z]").When(x => x.StartPrice != null);
        }
    }
}