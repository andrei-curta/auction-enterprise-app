using DataMapper.DAO;
using DomainModel.Models;
using FluentValidation;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class UserServiceTests
    {
        [Fact()]
        public void TestAddShouldThrowValidationException()
        {
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            var product = new User()
            {
                UserName = null
            };

            var service = new UserService(userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(product));
            Assert.IsType<ValidationException>(exception);
        }
    }
}