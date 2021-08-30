using System;
using System.Collections.Generic;
using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class UserValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("aaaa")]
        [InlineData("a")]
        public void TestIncorrectUsername(string username)
        {
            var user = new User()
            {
                UserName = username
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldHaveValidationErrorFor(a => a.UserName);
        }

        [Theory]
        [InlineData("aaaaa")]
        [InlineData("aa-a-aa")]
        public void TestValidUsername(string username)
        {
            var user = new User()
            {
                UserName = username
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.UserName);
        }

        [Theory]
        [InlineData("aaaaa")]
        [InlineData("aa-a-aa")]
        [InlineData("")]
        [InlineData("asdaasd@")]
        [InlineData("asd    aasd@")]
        public void TestIncorrectEmail(string email)
        {
            var user = new User()
            {
                Email = email
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldHaveValidationErrorFor(a => a.Email);
        }

        [Theory]
        [InlineData("asdaasd@yahoo")]
        [InlineData("asdaasd@yahoo.com")]
        [InlineData("asdaasd@yahoo.co.uk")]
        [InlineData("test@123.123.123.123")]
        public void TestValidEmail(string email)
        {
            var user = new User()
            {
                Email = email
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.Email);
        }

        [Theory]
        [InlineData("0747584239")]
        [InlineData(" 0747584239")]
        public void TestValidPhone(string phoneNumber)
        {
            var user = new User()
            {
                PhoneNumber = phoneNumber
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.PhoneNumber);
        }

        [Theory]
        [InlineData("0747 584 239")]
        [InlineData("0A747584239A")]
        [InlineData("0747-584-239")]
        [InlineData("0747.584.239")]
        [InlineData("🥃")]
        public void TestInvalidPhone(string phoneNumber)
        {
            var user = new User()
            {
                PhoneNumber = phoneNumber
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        public static IEnumerable<object[]> TestIsInRoleData =>
            new List<object[]>
            {
                new object[]
                {
                    "a", new List<Role>() { new Role() { NormalizedName = "A" } }, true
                },
                new object[]
                {
                    "A", new List<Role>() { new Role() { NormalizedName = "A" } }, true
                },
                new object[]
                {
                    "A ", new List<Role>() { new Role() { NormalizedName = "A" } }, true
                },
                new object[]
                {
                    "   A ", new List<Role>() { new Role() { NormalizedName = "A" } }, true
                },
                new object[]
                {
                    "A", new List<Role>() { }, false
                },
                new object[]
                {
                    "  a ",
                    new List<Role>() { new Role() { NormalizedName = "A" }, new Role() { NormalizedName = "B" } }, true
                },
                new object[]
                {
                    "   a ",
                    new List<Role>() { new Role() { NormalizedName = "B" } }, false
                },
                new object[]
                {
                    "A",
                    new List<Role>() { new Role() { NormalizedName = "B" } }, false
                },
            };

        [Theory]
        [MemberData(nameof(TestIsInRoleData))]
        public void TestIsInRole(string role, List<Role> roles, bool expected)
        {
            var user = new User()
            {
                Roles = roles
            };

            Assert.Equal(expected, user.IsInRole(role));
        }
    }
}