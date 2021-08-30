using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DomainModel.Models;
using DomainModel.Validators;
using DomainModel.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModelTests.Validators
{
    [ExcludeFromCodeCoverage]
    public class AuctionValidatorTests
    {
        public static IEnumerable<object[]> TestIncorrectStartDateData =>
            new List<object[]>
            {
                new object[]
                {
                    new DateTime(2020, 01, 01)
                },
                new object[]
                {
                    DateTime.Now.Subtract(new TimeSpan(0,0,0,1))
                },
                new object[]
                {
                    DateTime.Now.Subtract(new TimeSpan(1,0,0,1))
                },
                new object[]
                {
                    DateTime.MinValue
                },
            };

        [Theory]
        [MemberData(nameof(TestIncorrectStartDateData))]
        public void TestIncorrectStartDate(DateTime startDate)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, "RON"),
                StartDate = startDate
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldHaveValidationErrorFor(a => a.StartDate);
        }

        
        public static IEnumerable<object[]> TestCorrectStartDateData =>
            new List<object[]>
            {
                new object[]
                {
                    DateTime.Now.AddDays(1), DateTime.MaxValue
                },
                new object[]
                {
                    DateTime.Now, DateTime.MaxValue
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddDays(1)
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddSeconds(1)
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddMonths(1)
                },
                new object[]
                {
                    DateTime.Now.Subtract(new TimeSpan(0,0,0,1)), DateTime.Now.AddMonths(1)
                },
            };

        [Theory]
        [MemberData(nameof(TestCorrectStartDateData))]
        public void TestCorrectStartDate(DateTime startDate, DateTime endDate)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, "RON"),
                StartDate = startDate,
                EndDate = endDate
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.StartDate);
        }


        [Theory]
        [InlineData(0)]
        public void TestEmptyProductId(long productID)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, "RON"),
                ProductId = productID
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldHaveValidationErrorFor(a => a.ProductId);
        }

        [Theory]
        [InlineData(1)]
        public void TestNotEmptyProductId(long productID)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, "RON"),
                ProductId = productID
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.ProductId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1.1)]
        [InlineData(0)]
        public void TestInvalidAmount(decimal value)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(value, "RON")
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldHaveValidationErrorFor(a => a.StartPrice.Amount);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aaa")]
        [InlineData("a--")]
        [InlineData("a!a")]
        [InlineData("aaaa")]
        public void TestInvalidCurrency(string currency)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, currency)
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldHaveValidationErrorFor(a => a.StartPrice.Currency);
        }

        [Theory]
        [InlineData("EUR")]
        [InlineData("RON")]
        public void TestValidCurrency(string currency)
        {
            var auction = new Auction()
            {
                StartPrice = new Money(1, currency)
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.StartPrice.Currency);
        }

        [Fact]
        public void TestEmptyStartPrice()
        {
            var auction = new Auction()
            {
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldHaveValidationErrorFor(a => a.StartPrice);
        }

        public static IEnumerable<object[]> TestClosedData =>
            new List<object[]>
            {
                new object[]
                {
                    new DateTime(2020, 01, 01), true, true
                },
                new object[]
                {
                    new DateTime(2020, 01, 01), false, true
                },
                new object[]
                {
                    DateTime.Now.AddDays(1), false, false
                },
                new object[]
                {
                    DateTime.Now.AddDays(1), true, true
                },
                new object[]
                {
                    DateTime.Now.AddSeconds(1), true, true
                },
            };

        [Theory]
        [MemberData(nameof(TestClosedData))]
        public void TestClosed(DateTime endDate, bool closedByOwner, bool expected)
        {
            var auction = new Auction()
            {
                EndDate = endDate,
                ClosedByOwner = closedByOwner,
            };

            Assert.Equal(expected, auction.Closed);
        }
    }
}