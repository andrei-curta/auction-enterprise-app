using Xunit;
using ServiceLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using DataMapper.DAO;
using DomainModel.Models;
using FluentValidation;
using Moq;

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