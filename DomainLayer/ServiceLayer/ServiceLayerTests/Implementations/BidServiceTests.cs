using DataMapper.DAO;
using DomainModel.Models;
using DomainModel.ValueObjects;
using Moq;
using System;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class BidServiceTests
    {
        [Fact()]
        public void AddTestAuctionNotFound()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = -1
            };

            Auction auction = null;
            auctionDataServiceMock.Setup(x => x.GetByID(-1)).Returns(auction);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The auction was not found!", exception.Message);
        }

        [Fact()]
        public void AddTestAuctionNotStarted()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId
            };

            Auction auction = new Auction()
                {Id = auctionId, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5)};
            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The auction has not started yet!", exception.Message);
        }

        [Fact()]
        public void AddTestAuctionEnded()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId
            };

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.Subtract(new TimeSpan(0, 1, 1, 1))
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("The auction is closed, you cannot add a bid to it", exception.Message);
        }

        [Fact()]
        public void AddTestInvalidCurrency()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The bid must be placed in the same currency as the auction!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooLowNoPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId,
                BidValue = new Money(9, "RON")
            };

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            Bid previousBid = null;

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The amount must be greater than the one of the last bid!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooLowWithPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId,
                BidValue = new Money(10.01M, "RON")
            };

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            Bid previousBid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId,
                BidValue = new Money(10.1M, "RON")
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The amount must be greater than the one of the last bid!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooHighNoPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId,
                BidValue = new Money(12M, "RON")
            };

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            Bid previousBid = null;

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("You cannot bid with more than 10% more than the last bid!", exception.Message);
        }

        [Theory]
        [InlineData(10, 10.5, 12)]
        [InlineData(10.1, 10.5, 12)]
        public void AddTestBidAmountTooHighWithPreviousBid(decimal auctionStartPrice, decimal latestBidValue,
            decimal newBidValue)
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            long auctionId = 1;

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(auctionStartPrice, "RON")
            };

            Bid previousBid = new Bid()
            {
                AuctionId = auctionId,
                BidValue = new Money(latestBidValue, "RON")
            };

            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId,
                BidValue = new Money(newBidValue, "RON")
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("You cannot bid with more than 10% more than the last bid!", exception.Message);
        }

        [Fact()]
        public void UpdateTest()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object);

            var bid = new Bid();

            var exception = Record.Exception(() => service.Update(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("A bid once placed cannot be altered!", exception.Message);
        }
    }
}