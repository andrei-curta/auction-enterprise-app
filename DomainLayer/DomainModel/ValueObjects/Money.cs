// <copyright file="Money.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace DomainModel.ValueObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Value object to represent money having a value and a currency.
    /// </summary>
    public class Money : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The amount/value of money in the declared currency.</param>
        /// <param name="currency">The currency the monetary value is in.</param>
        public Money(decimal amount, string currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        [Required]
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Currency { get; private set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Currency} {this.Amount}";
        }

        /// <inheritdoc/>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return this.Amount;
            yield return this.Currency;
        }
    }
}