using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DomainModel.Models;
using DomainModel.Validators;
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
                    new DateTime(2020, 01,01)
  
                },
                new object[]
                {
                    DateTime.MinValue

                }
            };

        [Theory]
        [MemberData(nameof(TestIncorrectStartDateData))]

        public void TestIncorrectStartDate(DateTime startDate)
        {
            var auction = new Auction()
            {
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
            };

        [Theory]
        [MemberData(nameof(TestCorrectStartDateData))]

        public void TestCorrectStartDate(DateTime startDate, DateTime endDate)
        {
            var auction = new Auction()
            {
                StartDate = startDate,
                EndDate =  endDate
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
                ProductId = productID
            };

            var validationResult = new AuctionValidator().TestValidate(auction);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.ProductId);
        }
    }
}
