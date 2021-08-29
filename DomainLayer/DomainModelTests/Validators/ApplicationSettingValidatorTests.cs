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
        [InlineData("name asd asd")]
        [InlineData("name-asd-asd ")]

        public void TestIncorrectName(string name)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = name
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldHaveValidationErrorFor(appSetting => appSetting.Name);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("as")]

        public void TestNameTooShort(string name)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = name
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldHaveValidationErrorFor(appSetting => appSetting.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public void TestEmptyName(string name)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = name
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldHaveValidationErrorFor(appSetting => appSetting.Name);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("name_asd_asd")]
        [InlineData("name_asd-asd")]

        public void TestValidName(string name)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = name
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldNotHaveValidationErrorFor(appSetting => appSetting.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public void TestEmptyValue(string value)
        {
            var applicationSetting = new ApplicationSetting()
            {
                Name = "name",
                Value = value
            };

            var validationResult = new ApplicationSettingValidator().TestValidate(applicationSetting);

            validationResult.ShouldHaveValidationErrorFor(appSetting => appSetting.Value);
        }
    }
}