using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class CategoryValidatorTests
    {
        [Fact]
        public void TestEmptyName()
        {
            var category = new Category()
            {
            };

            var validationResult = new CategoryValidator().TestValidate(category);

            validationResult.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void TestMaxLenghtName()
        {
            var category = new Category()
            {
                Name = new string('a', 150)
            };

            var validationResult = new CategoryValidator().TestValidate(category);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void TestAboveMaxLenghtName()
        {
            var category = new Category()
            {
                Name = new string('a', 5000)
            };

            var validationResult = new CategoryValidator().TestValidate(category);

            validationResult.ShouldHaveValidationErrorFor(a => a.Name);
        }
    }
}