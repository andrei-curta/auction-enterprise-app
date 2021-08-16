using Xunit;
using ServiceLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using DataMapper.DAO;
using DomainModel.Models;
using Moq;

namespace ServiceLayer.Implementations.Tests
{
    public class AuctionServiceTests
    {
        [Fact()]
        public void AddTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        // [Fact()]
        // public void HasReachedMaxNumberOfOpenedAuctionsTest()
        // {
        //     Mock<AuctionDataService> auctionDataServiceMock =
        //         new Mock<AuctionDataService>();
        //
        //     auctionDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);
        //
        //     var service = new AuctionService(auctionDataServiceMock.Object);
        //
        //     var result = service.GetValueAsInt(name);
        //
        //     Assert.Equal(result, expectedValue);
        // }
        //
        // [Fact()]
        // public void StartPriceIsAboveThresholdTest()
        // {
        //     Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
        //         new Mock<ApplicationSettingDataService>();
        //
        //     string name = "aasda";
        //
        //     var data = new ApplicationSetting()
        //     {
        //         Name = name,
        //         Value = appSettingValue,
        //         Id = 1
        //     };
        //
        //     applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(data);
        //
        //     var auctionService = new AuctionService()
        //
        // }
    }
}