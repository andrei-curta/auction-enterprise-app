// <copyright file="ScoreValidator.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Validators
{
    using DomainModel.Models;
    using FluentValidation;

    /// <summary>
    /// Validator for <see cref="Score"/>.
    /// </summary>
    public class ScoreValidator : AbstractValidator<Score>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreValidator"/> class.
        /// </summary>
        public ScoreValidator()
        {
            this.RuleFor(x => x.AssignedToUser).NotEmpty();
            this.RuleFor(x => x.AssignedByUser).NotEmpty();

            this.RuleFor(x => x.ScoreValue).InclusiveBetween((ushort)1, (ushort)5);
        }
    }
}
