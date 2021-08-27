// <copyright file="Score.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the trustworthy score an user assigns to another user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Score
    {
        /// <summary>
        /// Gets or sets the Id of the Score.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user that the score is assigned to.
        /// </summary>
        [Required]
        public virtual User AssignedToUser { get; set; }

        /// <summary>
        /// Gets or sets the user that assigned the score.
        /// </summary>
        [Required]
        public virtual User AssignedByUser { get; set; }

        /// <summary>
        /// Gets or sets the actual score value.
        /// </summary>
        [Required]
        [Range(1, 5)]
        public ushort ScoreValue { get; set; }
    }
}