// <copyright file="AuctionPlacingRestrictionValidator.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Validators
{
    using DomainModel.Models;
    using FluentValidation;

    /// <summary>
    /// Class AuctionPlacingRestrictionValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{DomainModel.Models.AuctionPlacingRestriction}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Models.AuctionPlacingRestriction}" />
    public class AuctionPlacingRestrictionValidator : AbstractValidator<AuctionPlacingRestriction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionPlacingRestrictionValidator"/> class.
        /// </summary>
        public AuctionPlacingRestrictionValidator()
        {
            this.RuleFor(x => x.StartDate).NotEmpty().LessThan(x => x.EndDate).WithMessage("Start date cannot be after End date");
            this.RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date must be specified.");

            this.RuleFor(x => x.User).NotNull().NotEmpty();

            this.RuleFor(x => x.User.Id).NotEmpty().When(x => x.User != null);
        }
    }
}