using System;
using System.Collections.Generic;
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

            Assert.Equal(expectedValue, result);
        }

        public static IEnumerable<object[]> GetValueAsIntTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "1", 1
                },
                new object[]
                {
                    "0", 0
                },
                new object[]
                {
                    "-1", -1
                },
                new object[]
                {
                    int.MaxValue.ToString(), int.MaxValue
                },
                new object[]
                {
                    int.MinValue.ToString(), int.MinValue
                },
            };

        [Theory]
        [MemberData(nameof(GetValueAsIntTestData))]
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

            Assert.Equal(expectedValue, result);
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

            Assert.Equal(expectedValue, result);
        }

        public static IEnumerable<object[]> GetValueAsDecimalTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "1", 1
                },
                new object[]
                {
                    "0", 0
                },
                new object[]
                {
                    "-1", -1
                },
                new object[]
                {
                    decimal.MaxValue.ToString(), decimal.MaxValue
                },
                new object[]
                {
                    decimal.MinValue.ToString(), decimal.MinValue
                },
                new object[]
                {
                    "    2,00", 2
                },
            };

        [Theory]
        [MemberData(nameof(GetValueAsDecimalTestData))]
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

            Assert.Equal(expectedValue, result);
        }

        public static IEnumerable<object[]> GetValueAsDoubleTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "1", 1
                },
                new object[]
                {
                    "0", 0
                },
                new object[]
                {
                    "-1", -1
                },
                new object[]
                {
                    double.MaxValue.ToString(), double.MaxValue
                },
                new object[]
                {
                    double.MinValue.ToString(), double.MinValue
                },
                new object[]
                {
                    "    2,00", 2
                },
            };

        [Theory]
        [MemberData(nameof(GetValueAsDoubleTestData))]
        public void GetValueAsDoubleTest(string appSettingValue, double expectedValue)
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

            var result = service.GetValueAsDouble(name);

            Assert.Equal(expectedValue, result);
        }
    }
}