// <copyright file="Bid.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.ValueObjects;

    /// <summary>
    /// Class that represents a bid an user makes at an auction.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Bid
    {
        /// <summary>
        /// Gets or sets the Id for this Bid.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Id of the user that placed this bid.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the auctionId that the bid was placed in.
        /// </summary>
        [Required]
        public long AuctionId { get; set; }

        /// <summary>
        /// Gets or sets the date when the bid was added. Default is current datetime.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the bid value and currency for this bid.
        /// </summary>
        [Required]
        public Money BidValue { get; set; }
    }
}