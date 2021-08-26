using DataMapper.DAO;
using DomainModel.Models;
using FluentValidation;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class CategoryServiceTests
    {
        [Fact()]
        public void TestAddShouldThrowValidationError()
        {
            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            var category = new Category()
            {
                Name = null
            };

            var service = new CategoryService(categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(category));
            Assert.IsType<ValidationException>(exception);
        }
    }
}