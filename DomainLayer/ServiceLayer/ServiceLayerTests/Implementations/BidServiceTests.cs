using Xunit;
using ServiceLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using DataMapper.DAO;
using DomainModel.Models;
using DomainModel.ValueObjects;
using Moq;

namespace ServiceLayer.Implementations.Tests
{
    public class BidServiceTests
    {
        [Fact()]
        public void AddTestAuctionNotFound()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();

            var service = new BidService(auctionDataServiceMock.Object);

            var bid = new Bid() {AuctionId = -1};
            Auction auction = null;
            auctionDataServiceMock.Setup(x => x.GetByID(-1)).Returns(auction);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The auction was not found!", exception.Message);
        }

        [Fact()]
        public void AddTestAuctionNotStarted()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();

            long auctionId = 1;
            var bid = new Bid() {AuctionId = auctionId};
            Auction auction = new Auction()
                {Id = auctionId, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5)};
            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The auction has not started yet!", exception.Message);
        }

        [Fact()]
        public void AddTestAuctionEnded()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();

            long auctionId = 1;
            var bid = new Bid() {AuctionId = auctionId};
            Auction auction = new Auction()
                {Id = auctionId, 
                    StartDate = DateTime.Now.Subtract(new TimeSpan(1,1,1,1)), 
                    EndDate = DateTime.Now.Subtract(new TimeSpan(0, 1, 1, 1))};

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("The auction is closed, you cannot add a bid to it", exception.Message);
        }

        [Fact()]
        public void AddTestInvalidCurrency()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                AuctionId = auctionId,
                BidValue = new Money(10, "EUR")
            };

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The bid must be placed in the same currency as the auction!", exception.Message);
        }

        [Fact()]
        public void UpdateTest()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();

            var service = new BidService(auctionDataServiceMock.Object);

            var bid = new Bid();

            var exception = Record.Exception(() => service.Update(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("A bid once placed cannot be altered!", exception.Message);
        }
    }
}