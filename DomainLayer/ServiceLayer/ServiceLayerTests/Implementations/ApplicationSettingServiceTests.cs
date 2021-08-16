using DataMapper.DAO;
using DomainModel.Models;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class ApplicationSettingServiceTests
    {
        [Fact()]
        public void GetByNameTest()
        {
            var data = new ApplicationSetting()
            {
                Name = "BBB",
                Value = "123",
                Id = 1
            };


            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "aasda";
            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);

            var service = new ApplicationSettingService(applicationSettingDataServiceMock.Object);

            var result = service.GetByName(name);

            Assert.Equal(data, result);
        }
        

        [Theory]
        [InlineData("1", "1")]
        public void GetValueAsStringTest(string appSettingValue, string expectedValue)
        {
            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "aasda";

            var data = new ApplicationSetting()
            {
                Name = name,
                Value = appSettingValue,
                Id = 1
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);

            var service = new ApplicationSettingService(applicationSettingDataServiceMock.Object);

            var result = service.GetValueAsString(name);

            Assert.Equal(result, expectedValue);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData(" -1", -1)]
        public void GetValueAsIntTest(string appSettingValue, int expectedValue)
        {
            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "aasda";

            var data = new ApplicationSetting()
            {
                Name = name,
                Value = appSettingValue,
                Id = 1
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);

            var service = new ApplicationSettingService(applicationSettingDataServiceMock.Object);

            var result = service.GetValueAsInt(name);

            Assert.Equal(result, expectedValue);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("   True", true)]
        [InlineData("false", false)]
        [InlineData(" False ", false)]
        public void GetValueAsBoolTest(string appSettingValue, bool expectedValue)
        {
            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "aasda";

            var data = new ApplicationSetting()
            {
                Name = name,
                Value = appSettingValue,
                Id = 1
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);

            var service = new ApplicationSettingService(applicationSettingDataServiceMock.Object);

            var result = service.GetValueAsBool(name);

            Assert.Equal(result, expectedValue);
        }

        [Theory]
        [InlineData("2,14", 2.14)]
        public void GetValueAsDecimalTest(string appSettingValue, decimal expectedValue)
        {
            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "aasda";

            var data = new ApplicationSetting()
            {
                Name = name,
                Value = appSettingValue,
                Id = 1
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);

            var service = new ApplicationSettingService(applicationSettingDataServiceMock.Object);

            var result = service.GetValueAsDecimal(name);

            Assert.Equal(result, expectedValue);
        }
    }
}