using Xunit;
using DomainModel.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.Models;
using FluentValidation.TestHelper;

namespace DomainModel.Validators.Tests
{
    public class AuctionPlacingRestrictionValidatorTests
    {
        public static IEnumerable<object[]> TestEmptyUserData =>
            new List<object[]>
            {
                new object[]
                {
                    null, false
                },
                new object[]
                {
                    new User(), true
                },
            };


        [Theory]
        [MemberData(nameof(TestEmptyUserData))]
        public void TestEmptyUser(User user, bool valid)
        {
            var auctionPlacingRestriction = new AuctionPlacingRestriction()
            {
                User = user
            };

            var validationResult = new AuctionPlacingRestrictionValidator().TestValidate(auctionPlacingRestriction);

            if (valid)
            {
                validationResult.ShouldNotHaveValidationErrorFor(x => x.User);
            }
            else
            {
                validationResult.ShouldHaveValidationErrorFor(x => x.User);
            }
        }

        public static IEnumerable<object[]> TestEmptyUserIdData =>
            new List<object[]>
            {
                new object[]
                {
                    null, true
                },
                new object[]
                {
                    new User() { Id = null }, false
                },
                new object[]
                {
                    new User() { Id = "1" }, true
                },
            };


        [Theory]
        [MemberData(nameof(TestEmptyUserIdData))]
        public void TestEmptyUserId(User user, bool valid)
        {
            var auctionPlacingRestriction = new AuctionPlacingRestriction()
            {
                User = user
            };

            var validationResult = new AuctionPlacingRestrictionValidator().TestValidate(auctionPlacingRestriction);

            if (valid)
            {
                validationResult.ShouldNotHaveValidationErrorFor(x => x.User.Id);
            }
            else
            {
                validationResult.ShouldHaveValidationErrorFor(x => x.User.Id);
            }
        }


        public static IEnumerable<object[]> TestStartDateData =>
            new List<object[]>
            {
                new object[]
                {
                    DateTime.Now.AddDays(1), DateTime.MaxValue, true
                },
                new object[]
                {
                    DateTime.Now, DateTime.MaxValue, true
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddDays(1), true
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddSeconds(1), true
                },
                new object[]
                {
                    DateTime.Now, DateTime.Now.AddMonths(1), true
                },
                new object[]
                {
                    DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 1)), DateTime.Now.AddMonths(1), true
                },

                new object[]
                {
                    DateTime.MaxValue, 
                    DateTime.Now.AddDays(1), 
                    false
                },
                new object[]
                {
                    DateTime.MaxValue,
                    DateTime.Now,
                    false
                },
                new object[]
                {
                    DateTime.Now.AddDays(1), 
                    DateTime.Now, 
                    false
                },
                new object[]
                {
                    DateTime.Now.AddSeconds(1), 
                    DateTime.Now, 
                    false
                },
                new object[]
                {
                    DateTime.Now.AddMonths(1),
                    DateTime.Now,
                    false
                },
                new object[]
                {
                    DateTime.Now.AddMonths(1), 
                    DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 1)), 
                    false
                },
            };

        [Theory]
        [MemberData(nameof(TestStartDateData))]
        public void TestStartDate(DateTime startDate, DateTime endDate, bool valid)
        {
            var auction = new AuctionPlacingRestriction()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var validationResult = new AuctionPlacingRestrictionValidator().TestValidate(auction);

            if (valid)
            {
                validationResult.ShouldNotHaveValidationErrorFor(a => a.StartDate);
            }
            else
            {
                validationResult.ShouldHaveValidationErrorFor(a => a.StartDate);
            }
        }
    }
}