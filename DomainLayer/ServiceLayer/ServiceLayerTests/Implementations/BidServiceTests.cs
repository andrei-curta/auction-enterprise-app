using DataMapper.DAO;
using DomainModel.Models;
using DomainModel.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
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
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

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
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            long auctionId = 1;
            var bid = new Bid()
            {
                UserId = "a",
                AuctionId = auctionId
            };

            Auction auction = new Auction()
                { Id = auctionId, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5) };
            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The auction has not started yet!", exception.Message);
        }

        [Fact()]
        public void AddTestAuctionEnded()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("The auction is closed, you cannot add a bid to it", exception.Message);
        }

        [Fact()]
        public void AddTestInvalidCurrency()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The bid must be placed in the same currency as the auction!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooLowNoPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The amount must be greater than the one of the last bid!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooLowWithPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("The amount must be greater than the one of the last bid!", exception.Message);
        }

        [Fact()]
        public void AddTestBidAmountTooHighNoPreviousBid()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("You cannot bid with more than 10% more than the last bid!", exception.Message);
        }

        [Theory]
        [InlineData(10, 10.5, 12)]
        [InlineData(10.1, 10.5, 12)]
        [InlineData(0.000001, 10.5, 12)]
        [InlineData(0.000001, 0.000001, 12)]
        [InlineData(0.000001, 0.000001, 0.00001)]
        public void AddTestBidAmountTooHighWithPreviousBid(decimal auctionStartPrice, decimal latestBidValue,
            decimal newBidValue)
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

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

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("You cannot bid with more than 10% more than the last bid!", exception.Message);
        }


        public static IEnumerable<object[]> AddTestBidUserNotInRoleData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { }
                    },
                    false
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "AUCTIONER"}
                    },
                    false
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "BIDDER"}
                    },
                    true
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "BIDDER"},
                        new Role() { NormalizedName = "AUCTIONER"}
                    },
                    true
                },
            };

        [Theory]
        [MemberData(nameof(AddTestBidUserNotInRoleData))]
        public void AddTestBidUserRole(List<Role> roles, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            long auctionId = 1;
            string userId = "a";

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            Bid previousBid = new Bid()
            {
                AuctionId = auctionId,
                BidValue = new Money(10.5M, "RON"),
            };

            var bid = new Bid()
            {
                UserId = userId,
                AuctionId = auctionId,
                BidValue = new Money(11, "RON")
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);
            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User() { Roles = roles });

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(bid));

            if (valid)
            {
                Assert.Null(exception);
            }
            else
            {
                Assert.IsType<UnauthorizedAccessException>(exception);
                Assert.Equal("You are not in the role that allows bidding!", exception.Message);
            }
        }

        [Fact()]
        public void UpdateTest()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object);

            var bid = new Bid();

            var exception = Record.Exception(() => service.Update(bid));
            Assert.IsType<Exception>(exception);
            Assert.Equal("A bid once placed cannot be altered!", exception.Message);
        }

        [Fact()]
        public void UpdateTestLogging()
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();
            Mock<ILogger<BidService>> loggerMock = new Mock<ILogger<BidService>>();


            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object, loggerMock.Object);

            var bid = new Bid();

            var exception = Record.Exception(() => service.Update(bid));

            loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        public static IEnumerable<object[]> AddTestLoggingData =>
           new List<object[]>
           {
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { }
                    },
                    false
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "AUCTIONER"}
                    },
                    false
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "BIDDER"}
                    },
                    true
                },
                new object[]
                {
                    new List<Role>()
                    {
                        new Role() { NormalizedName = "BIDDER"},
                        new Role() { NormalizedName = "AUCTIONER"}
                    },
                    true
                },
           };

        [Theory]
        [MemberData(nameof(AddTestBidUserNotInRoleData))]
        public void AddTestLogging(List<Role> roles, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock = new Mock<AuctionDataService>();
            Mock<BidDataService> bidDataServiceMock = new Mock<BidDataService>();
            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();
            Mock<ILogger<BidService>> loggerMock = new Mock<ILogger<BidService>>();

            long auctionId = 1;
            string userId = "a";

            Auction auction = new Auction()
            {
                Id = auctionId,
                StartDate = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)),
                EndDate = DateTime.Now.AddDays(1),
                StartPrice = new Money(10, "RON")
            };

            Bid previousBid = new Bid()
            {
                AuctionId = auctionId,
                BidValue = new Money(10.5M, "RON"),
            };

            var bid = new Bid()
            {
                UserId = userId,
                AuctionId = auctionId,
                BidValue = new Money(11, "RON")
            };

            auctionDataServiceMock.Setup(x => x.GetByID(auctionId)).Returns(auction);
            bidDataServiceMock.Setup(x => x.GetLatestBidByAuction(auctionId)).Returns(previousBid);
            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User() { Roles = roles });

            var service = new BidService(bidDataServiceMock.Object, auctionDataServiceMock.Object,
                userDataServiceMock.Object, loggerMock.Object);

            var exception = Record.Exception(() => service.Add(bid));

            if (valid)
            {
                loggerMock.Verify(
                    x => x.Log(
                        It.IsAny<LogLevel>(),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => true),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);
            }
            else
            {
                loggerMock.Verify(
                    x => x.Log(
                        It.IsAny<LogLevel>(),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => true),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
            }
        }
    }
}