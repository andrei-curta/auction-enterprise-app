using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class ProductValidatorTests
    {
        [Fact]
        public void TestEmptyName()
        {
            var product = new Product()
            {
               Name = null
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void TestMaxLengthName()
        {
            var product = new Product()
            {
                Name = new string('a', 150)
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void TestExcedingMaxLengthName()
        {
            var product = new Product()
            {
                Name = new string('a', 151)
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void TestEmptyDescription()
        {
            var product = new Product()
            {
                Description = null
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldHaveValidationErrorFor(a => a.Description);
        }

        [Fact]
        public void TestMaxLengthDescription()
        {
            var product = new Product()
            {
                Description = new string('a', 1000)
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.Description);
        }

        [Fact]
        public void TestExcedingMaxLengthDescription()
        {
            var product = new Product()
            {
                Description = new string('a', 1001)
            };

            var validationResult = new ProductValidator().TestValidate(product);

            validationResult.ShouldHaveValidationErrorFor(a => a.Description);
        }
    }
}