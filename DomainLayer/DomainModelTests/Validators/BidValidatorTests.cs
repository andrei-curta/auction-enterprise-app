using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class BidValidatorTests
    {
        [Fact]
        public void TestEmptyUserId()
        {
            var bid = new Bid()
            {
            };

            var validationResult = new BidValidator().TestValidate(bid);

            validationResult.ShouldHaveValidationErrorFor(a => a.UserId);
        }
    }
}