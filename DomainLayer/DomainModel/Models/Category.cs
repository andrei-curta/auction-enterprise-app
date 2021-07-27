// <copyright file="Category.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent categories for this category.
        /// </summary>
        public List<Category> ParentCategories { get; set; }
    }
}
