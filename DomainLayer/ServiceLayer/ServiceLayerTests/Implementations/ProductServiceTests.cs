using DataMapper.DAO;
using DomainModel.Models;
using FluentValidation;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class ProductServiceTests
    {
        [Fact()]
        public void TestAddShouldThrowValidationException()
        {
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            var product = new Product()
            {
                Name = null
            };

            var service = new ProductService(productDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(product));
            Assert.IsType<ValidationException>(exception);
        }
    }
}