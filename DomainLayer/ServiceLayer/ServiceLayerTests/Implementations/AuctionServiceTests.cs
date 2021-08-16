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

        [Fact()]
        public void HasReachedMaxNumberOfOpenedAuctionsTest()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataService = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string name = "MaxUnfinishedAuctions";
            string userId = "1";
            var appSettingData = new ApplicationSetting()
            {
                Name = "BBB",
                Value = "123",
                Id = 1
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction(),
                new Auction()
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);
            
            var service = new AuctionService(auctionDataServiceMock.Object, productDataService.Object, applicationSettingDataServiceMock.Object);
            var result = service.HasReachedMaxNumberOfOpenedAuctions(userId);
        
            Assert.False(result);
        }

    }
}