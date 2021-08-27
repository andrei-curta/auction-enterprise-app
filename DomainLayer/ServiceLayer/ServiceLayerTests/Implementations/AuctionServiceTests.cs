using DataMapper.DAO;
using DomainModel.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class AuctionServiceTests
    {
        [Fact()]
        public void HasReachedMaxNumberOfOpenedAuctionsTest()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataService = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            string name = "MaxUnfinishedAuctions";
            string userId = "1";
            var appSettingData = new ApplicationSetting()
            {
                Name = name,
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

            var service = new AuctionService(auctionDataServiceMock.Object, productDataService.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object);
            var result = service.HasReachedMaxNumberOfOpenedAuctions(userId);

            Assert.False(result);
        }
        
        [Fact()]
        public void AddTestExcededMaxDuration()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataService = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();


            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            var appSettingData = new ApplicationSetting()
            {
                Name = name,
                Value = "7",
                Id = 1
            };

            var auction = new Auction()
            {
                UserId = userId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                ClosedByOwner = false
            };


            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name,
                Value = "123",
                Id = 1,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() {ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now},
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataService.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object);
            service.Add(auction);

            var exception = Record.Exception(() => service.Add(auction));
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The max duration of an auction was exceded.", exception.Message);
        }
    }
}