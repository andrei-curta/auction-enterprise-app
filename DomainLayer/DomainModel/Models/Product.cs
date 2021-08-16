// <copyright file="Product.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using System.Collections.Generic;

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel.ValueObjects;

    /// <summary>
    /// A product that is to be sold at aan auction.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the Id of this product.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this product.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of this product.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner of this product.
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner of this product.
        /// </summary>
        [Required]
        public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets categories that the product is part of.
        /// </summary>
        public virtual List<Category> Categories { get; set; }
    }
}