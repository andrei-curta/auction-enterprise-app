using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class UserValidatorTests
    {
        [Fact()]
        public void TestEmptyUsername()
        {
            var user = new User()
            {
                UserName = null
            };

            var validationResult = new UserValidator().TestValidate(user);

            validationResult.ShouldHaveValidationErrorFor(a => a.UserName);
        }
    }
}