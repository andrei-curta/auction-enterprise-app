// <copyright file="AuctionPlacingRestriction.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System;

    /// <summary>
    /// Once an user's score drops below a value, it cannot place an auction for a certain amount of time.
    /// </summary>
    public class AuctionPlacingRestriction
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user that is sanctioned.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the date and time the restrictions start.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time the restrictions end.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}