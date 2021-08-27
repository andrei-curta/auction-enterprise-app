using DomainModel.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainModel.Validators.Tests
{
    public class ScoreValidatorTests
    {
        [Fact]
        public void TestEmptyAssignedByUser()
        {
            var score = new Score()
            {
                AssignedByUser = null
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldHaveValidationErrorFor(a => a.AssignedByUser);
        }

        [Fact]
        public void TestNotEmptyAssignedByUser()
        {
            var score = new Score()
            {
                AssignedByUser = new User()
                {
                    Id = "1"
                }
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.AssignedByUser);
        }

        [Fact]
        public void TestEmptyAssignedToUser()
        {
            var score = new Score()
            {
                AssignedToUser = null
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldHaveValidationErrorFor(a => a.AssignedToUser);
        }

        [Fact]
        public void TestNotEmptyAssignedToUser()
        {
            var score = new Score()
            {
                AssignedToUser = new User()
                {
                    Id = "1"
                }
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.AssignedToUser);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void TestValidScoreValue(ushort scoreValue)
        {
            var score = new Score()
            {
                ScoreValue = scoreValue
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldNotHaveValidationErrorFor(a => a.ScoreValue);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(ushort.MaxValue)]
        [InlineData(ushort.MinValue)]
        [InlineData(0)]
        public void TestInvalidScoreValue(ushort scoreValue)
        {
            var score = new Score()
            {
                ScoreValue = scoreValue
            };

            var validationResult = new ScoreValidator().TestValidate(score);

            validationResult.ShouldHaveValidationErrorFor(a => a.ScoreValue);
        }

    }
}