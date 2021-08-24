// <copyright file="ValueObject.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DomainModel.ValueObjects
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for value objects.
    /// </summary>
    public abstract class ValueObject
    {
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = this.GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="left">Left side of the equality comparison.</param>
        /// <param name="right">Right side of the equality comparison.</param>
        /// <returns>True if the elements are equal, false otherwise.</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        /// <summary>
        /// The reverse of <see cref="EqualOperator"/>.
        /// </summary>
        /// <param name="left">Left side of the equality comparison.</param>
        /// <param name="right">Right side of the equality comparison.</param>
        /// <returns>True if the elements are not equal, false otherwise.</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        /// <summary>
        /// Gets the atomic values.
        /// </summary>
        /// <returns>The atomic values.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}
