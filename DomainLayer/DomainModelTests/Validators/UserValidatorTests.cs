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
    }
}