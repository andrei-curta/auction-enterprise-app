using Xunit;
using DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.ValueObjects.Tests
{
    public class MoneyTests
    {
        [Theory]
        [InlineData(10, "RON", "RON 10")]
        [InlineData(10.5, "RON", "RON 10,5")]
        [InlineData(-10.5, "RON", "RON -10,5")]
        public void ToStringTest(decimal amount, string currency, string expected)
        {
            Money money = new Money(amount, currency);

            Assert.Equal(money.ToString(), expected);
        }

        public static IEnumerable<object[]> EqualTestData =>
            new List<object[]>
            {
                new object[]
                {
                   new Money(10, "RON"),
                   new Money(10, "RON"),
                   true
                },
                new object[]
                {
                    new Money(10, "RON"),
                    new Money(10, "EUR"),
                    false
                },
                new object[]
                {
                    new Money(11, "EUR"),
                    new Money(10, "EUR"),
                    false
                },
                new object[]
                {
                    new Money(0, "EUR"),
                    new Money(0, "EUR"),
                    true
                },
                new object[]
                {
                    new Money(0, "RON"),
                    new Money(0, "EUR"),
                    false
                },
            };

        [Theory]
        [MemberData(nameof(EqualTestData))]
        public void EqualTest(Money money1, Money money2, bool expected)
        {
            Assert.Equal(money1.Equals(money2), expected);
        }
    }
}