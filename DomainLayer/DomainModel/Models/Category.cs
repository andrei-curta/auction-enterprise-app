// <copyright file="Category.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Product category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the Id for this category.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of this category.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent categories for this category.
        /// </summary>
        public virtual List<Category> ParentCategories { get; set; }
    }
}