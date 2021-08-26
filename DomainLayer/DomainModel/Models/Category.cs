// <copyright file="Category.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Product category.
    /// </summary>
    [ExcludeFromCodeCoverage]
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
        /// Gets or sets the parent categories.
        /// </summary>
        public HashSet<Category> ParentCategories { get; set; }

        /// <summary>
        /// Gets or sets the subcategories.
        /// </summary>
        public virtual HashSet<Category> SubCategories { get; set; }

        /// <summary>
        /// Gets or sets the list of products in the category.
        /// </summary>
        public virtual List<Product> Products { get; set; }
    }
}