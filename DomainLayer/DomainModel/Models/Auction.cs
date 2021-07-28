﻿// <copyright file="Auction.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace DomainModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class that represents an auction.
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Gets or sets the id for this auction.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the start date of the auction.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of this auction. No more bids can be placed after this date and the auction is finalized.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the date when this auction was created.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the product that is auctioned in this auction.
        /// </summary>
        [Required]
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this auction was closed by the owner.
        /// </summary>
        [Required]
        public bool ClosedByOwner { get; set; } = false;
    }
}