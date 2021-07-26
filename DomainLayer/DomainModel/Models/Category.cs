// <copyright file="Category.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace DomainModel.Models
{
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent categories for this category.
        /// </summary>
        public List<Category> ParentCategories { get; set; }
    }
}
