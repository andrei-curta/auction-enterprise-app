using System.Diagnostics.CodeAnalysis;
using DomainModel.Models;
using DomainModel.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModelTests.Validators
{
    [ExcludeFromCodeCoverage]
    public class ApplicationSettingValidatorTests
    {
        [Theory]
        [InlineData("!@!@")]
        [InlineData("")]
        [InlineData("@#$%")]
        [InlineData("A&")]
        [InlineData(null)]
      
        public void TestIncorrectName(string name)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = name
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldHaveValidationErrorFor(appSetting => appSetting.Name);
        }
    }
}