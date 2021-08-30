using DataMapper.DAO;
using DomainModel.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel.ValueObjects;
using Microsoft.Extensions.Logging;
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

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();


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
                new Auction() { StartPrice = new Money(1, "RON"), },
                new Auction() { StartPrice = new Money(1, "RON"), }
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataService.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);
            var result = service.HasReachedMaxNumberOfOpenedAuctions(userId);

            Assert.False(result);
        }

        public static IEnumerable<object[]> AddTestExcededMaxDurationData =>
            new List<object[]>
            {
                new object[]
                {
                    new DateTime(2020, 01, 01), new DateTime(2020, 07, 01), "7", true
                },
                new object[]
                {
                    new DateTime(2020, 01, 01), new DateTime(2020, 07, 02), "5", false
                },
                new object[]
                {
                    new DateTime(2020, 01, 01), new DateTime(2020, 08, 30), "7", false
                },
            };

        [Theory]
        [MemberData(nameof(AddTestExcededMaxDurationData))]
        public void AddTestExcededMaxDuration(DateTime auctionStartDate, DateTime auctionEndDate,
            string auctionMaxDurationMonths, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


            var appSettingData = new ApplicationSetting()
            {
                Name = name,
                Value = auctionMaxDurationMonths,
                Id = 1
            };

            var auction = new Auction()
            {
                StartPrice = new Money(1, "RON"),
                UserId = userId,
                StartDate = auctionStartDate,
                EndDate = auctionEndDate,
                ClosedByOwner = false,
                ProductId = productId
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product() { Id = productId });

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User()
                { Roles = new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } } });

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            if (valid)
            {
                Assert.NotEqual("The max duration of an auction was exceded.", exception.Message);
            }
            else
            {
                Assert.Equal("The max duration of an auction was exceded.", exception.Message);
            }
        }

        public static IEnumerable<object[]> AddTestBelowMinStartPriceData =>
            new List<object[]>
            {
                new object[]
                {
                    10, "10", true
                },
                new object[]
                {
                    11, "10", true
                },
                new object[]
                {
                    decimal.MaxValue, "10", true
                },
                new object[]
                {
                    10, "11", false
                },
                new object[]
                {
                    10, "11,1", false
                },
                new object[]
                {
                    11.099999, "11,1", false
                },
            };

        [Theory]
        [MemberData(nameof(AddTestBelowMinStartPriceData))]
        public void AddTestMinStartPrice(decimal startPrice, string minStartPrice, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(startPrice, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = minStartPrice,
                Id = 3,
            };

            string maxOpenedAuctionsPerCategory = "MaxOpenedAuctionsPerCategory";
            var appSettingMaxOpenedAuctionsPerCategory = new ApplicationSetting()
            {
                Name = maxOpenedAuctionsPerCategory,
                Value = "1",
                Id = 3,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(maxOpenedAuctionsPerCategory))
                .Returns(appSettingMaxOpenedAuctionsPerCategory);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product()
                { Id = productId, Categories = new HashSet<Category>() });

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User()
                { Roles = new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } } });

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            string exceptionMessage = $"The price set is below the threshold of {minStartPrice}";

            if (valid)
            {
                Assert.Null(exception);
            }
            else
            {
                Assert.Equal(exceptionMessage, exception.Message);
            }
        }

        public static IEnumerable<object[]> AddTestOpenedAuctionsData =>
            new List<object[]>
            {
                new object[]
                {
                    9, "10", true
                },
                new object[]
                {
                    9999, "10000", true
                },
                new object[]
                {
                    1, "2", true
                },
                new object[]
                {
                    0, "1", true
                },
                new object[]
                {
                    1, "1", true
                },
                new object[]
                {
                    2, "1", false
                },
            };

        [Theory]
        [MemberData(nameof(AddTestOpenedAuctionsData))]
        public void AddTestOpenedAuctions(int numberOfUserAuctions, string maxNumberOfUserAuctions, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(10, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = maxNumberOfUserAuctions,
                Id = 1,
            };

            string maxOpenedAuctionsPerCategory = "MaxOpenedAuctionsPerCategory";
            var appSettingMaxOpenedAuctionsPerCategory = new ApplicationSetting()
            {
                Name = maxOpenedAuctionsPerCategory,
                Value = "1",
                Id = 3,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = "9",
                Id = 3,
            };

            var userAuctionsData = Enumerable
                .Repeat(new Auction() { ClosedByOwner = false, EndDate = DateTime.Now.AddDays(1) },
                    numberOfUserAuctions).ToList();

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(maxOpenedAuctionsPerCategory))
                .Returns(appSettingMaxOpenedAuctionsPerCategory);


            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product()
                { Id = productId, Categories = new HashSet<Category>() });

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User()
                { Roles = new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } } });

            categoryDataServiceMock.Setup(x => x.GetNumberOfOpenedAuctionsByCategory(userId))
                .Returns(new Dictionary<long, int>());

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            string exceptionMessage = $"You have reached the max number of open auctions;";

            if (valid)
            {
                Assert.Null(exception);
            }
            else
            {
                Assert.Equal(exceptionMessage, exception.Message);
            }
        }

        public static IEnumerable<object[]> UpdateTestClosedAuctionData =>
            new List<object[]>
            {
                new object[]
                {
                    new Auction()
                    {
                        Id = 1,
                        ClosedByOwner = true
                    },
                    new Auction()
                    {
                        Id = 1
                    },
                },
                new object[]
                {
                    new Auction()
                    {
                        Id = 1,
                        ClosedByOwner = true,
                        EndDate = DateTime.Now.Subtract(new TimeSpan(1))
                    },
                    new Auction()
                    {
                        Id = 1
                    },
                },
                // new object[]
                // {
                //     new Auction()
                //     {
                //         Id = 1,
                //         ClosedByOwner = false,
                //         EndDate = DateTime.Now.Subtract(new TimeSpan(1,1,1,1))
                //     },
                //     new Auction()
                //     {
                //         Id = 1,
                //     },
                // }
            };

        [Theory]
        [MemberData(nameof(UpdateTestClosedAuctionData))]
        public void UpdateTestClosedAuction(Auction dbAuction, Auction auction)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            auctionDataServiceMock.Setup(x => x.GetByID(auction.Id)).Returns(dbAuction);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Update(auction));

            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("The auction is closed, you cannot update it!", exception.Message);
        }


        public static IEnumerable<object[]> UpdateTestSholdUpdateData =>
            new List<object[]>
            {
                new object[]
                {
                    new Auction()
                    {
                        Id = 1,
                        ClosedByOwner = false,
                        EndDate = DateTime.Now.AddDays(1)
                    },
                    new Auction()
                    {
                        Id = 1,
                    },
                }
            };

        [Theory]
        [MemberData(nameof(UpdateTestSholdUpdateData))]
        public void UpdateTestSholdUpdate(Auction dbAuction, Auction auction)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            auctionDataServiceMock.Setup(x => x.GetByID(auction.Id)).Returns(dbAuction);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            service.Update(auction);

            auctionDataServiceMock.Verify(mock => mock.Update(It.IsAny<Auction>()), Times.Once);
        }

        [Fact]
        public void AddTestHasAuctionPlacingRestrictions()
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(12, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = "11",
                Id = 3,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product() { Id = productId });
            auctionPlacingDataServiceMock.Setup(x => x.HasActiveAuctionPlacingRestrictions(userId)).Returns(true);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            Assert.IsType<Exception>(exception);
            Assert.Equal("You have an active auction placing restriction. Try later.", exception.Message);
        }

        public static IEnumerable<object[]> AddTestRolesData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Role>(),
                    false
                },
                new object[]
                {
                    new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } },
                    true
                },
                new object[]
                {
                    new List<Role>() { new Role() { NormalizedName = "BIDDER" } },
                    false
                },
                new object[]
                {
                    new List<Role>()
                        { new Role() { NormalizedName = "AUCTIONER" }, new Role() { NormalizedName = "BIDDER" } },
                    true
                },
            };

        [Theory]
        [MemberData(nameof(AddTestRolesData))]
        public void AddTestRoles(List<Role> roles, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(12, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = "11",
                Id = 3,
            };

            string maxOpenedAuctionsPerCategory = "MaxOpenedAuctionsPerCategory";
            var appSettingMaxOpenedAuctionsPerCategory = new ApplicationSetting()
            {
                Name = maxOpenedAuctionsPerCategory,
                Value = "1",
                Id = 3,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(maxOpenedAuctionsPerCategory))
                .Returns(appSettingMaxOpenedAuctionsPerCategory);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product()
                { Id = productId, Categories = new HashSet<Category>() });
            auctionPlacingDataServiceMock.Setup(x => x.HasActiveAuctionPlacingRestrictions(userId)).Returns(false);

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User() { Roles = roles });

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            if (valid)
            {
                Assert.Null(exception);
            }
            else
            {
                Assert.IsType<UnauthorizedAccessException>(exception);
                Assert.Equal("You do not have the necessary role to add an auction!", exception.Message);
            }
        }


        public static IEnumerable<object[]> AddTestRolesMaxOpenedAuctionsPerCategoryData =>
            new List<object[]>
            {
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, } },
                    new Dictionary<long, int>() { { 1, 1 } },
                    "1",
                    true
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, } },
                    new Dictionary<long, int>() { },
                    "1",
                    true
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, } },
                    new Dictionary<long, int>() { { 1, 2 } },
                    "1",
                    false
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, }, new Category() { Id = 2, } },
                    new Dictionary<long, int>() { { 1, 2 } , { 2, 3 }},
                    "2",
                    false
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, }, new Category() { Id = 2, } },
                    new Dictionary<long, int>() { { 1, 3 } , { 2, 3 }},
                    "2",
                    false
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, }, new Category() { Id = 2, } },
                    new Dictionary<long, int>() { { 1, 3 } , { 2, 3 }},
                    "3",
                    true
                },
                new object[]
                {
                    new HashSet<Category>() { new Category() { Id = 1, }, new Category() { Id = 2, }, new Category() { Id = 3, }, new Category() { Id = 4, } },
                    new Dictionary<long, int>() { { 1, 3 } , { 2, 3 }},
                    "3",
                    true
                },
            };

        [Theory]
        [MemberData(nameof(AddTestRolesMaxOpenedAuctionsPerCategoryData))]
        public void AddTestRolesMaxOpenedAuctionsPerCategory(HashSet<Category> productCategories,
            Dictionary<long, int> categoriesWithNumberOfProducts, string maxOpenedAuctionsPerCategoryValue, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(12, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = "11",
                Id = 3,
            };

            string maxOpenedAuctionsPerCategory = "MaxOpenedAuctionsPerCategory";
            var appSettingMaxOpenedAuctionsPerCategory = new ApplicationSetting()
            {
                Name = maxOpenedAuctionsPerCategory,
                Value = maxOpenedAuctionsPerCategoryValue,
                Id = 3,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(maxOpenedAuctionsPerCategory))
                .Returns(appSettingMaxOpenedAuctionsPerCategory);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product()
                { Id = productId, Categories = productCategories });
            auctionPlacingDataServiceMock.Setup(x => x.HasActiveAuctionPlacingRestrictions(userId)).Returns(false);

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User()
                { Roles = new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } } });

            categoryDataServiceMock.Setup(x => x.GetNumberOfOpenedAuctionsByCategory(userId))
                .Returns(categoriesWithNumberOfProducts);

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

            if (valid)
            {
                Assert.Null(exception);
            }
            else
            {
                Assert.IsType<Exception>(exception);
                Assert.Equal("Too many opened auctions in category!", exception.Message);
            }
        }

        public static IEnumerable<object[]> AddTestLoggingData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Role>(),
                    false
                },
                new object[]
                {
                    new List<Role>() { new Role() { NormalizedName = "AUCTIONER" } },
                    true
                },
                new object[]
                {
                    new List<Role>() { new Role() { NormalizedName = "BIDDER" } },
                    false
                },
                new object[]
                {
                    new List<Role>()
                        { new Role() { NormalizedName = "AUCTIONER" }, new Role() { NormalizedName = "BIDDER" } },
                    true
                },
            };

        [Theory]
        [MemberData(nameof(AddTestLoggingData))]
        public void AddTestLogging(List<Role> roles, bool valid)
        {
            Mock<AuctionDataService> auctionDataServiceMock =
                new Mock<AuctionDataService>();
            Mock<ProductDataService> productDataServiceMock = new Mock<ProductDataService>();

            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();

            Mock<UserDataService> userDataServiceMock = new Mock<UserDataService>();

            Mock<CategoryDataService> categoryDataServiceMock = new Mock<CategoryDataService>();

            Mock<ILogger<AuctionService>> loggerMock = new Mock<ILogger<AuctionService>>();

            string userId = "1";
            string name = "AuctionMaxDurationMonths";
            long productId = 1;


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
                ClosedByOwner = false,
                ProductId = productId,
                StartPrice = new Money(12, "RON")
            };

            string name2 = "MaxUnfinishedAuctions";
            var appSettingMaxUnfinishedAuctions = new ApplicationSetting()
            {
                Name = name2,
                Value = "123",
                Id = 1,
            };

            string auctionMinStartPrice = "AuctionMinStartPrice";
            var appSettingAuctionMinStartPrice = new ApplicationSetting()
            {
                Name = auctionMinStartPrice,
                Value = "11",
                Id = 3,
            };

            string maxOpenedAuctionsPerCategory = "MaxOpenedAuctionsPerCategory";
            var appSettingMaxOpenedAuctionsPerCategory = new ApplicationSetting()
            {
                Name = maxOpenedAuctionsPerCategory,
                Value = "1",
                Id = 3,
            };

            var userAuctionsData = new List<Auction>()
            {
                new Auction() { ClosedByOwner = true, EndDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now },
            };

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name)).Returns(appSettingData);
            auctionDataServiceMock.Setup(x => x.GetAuctionsByUserId(userId)).Returns(userAuctionsData);

            applicationSettingDataServiceMock.Setup(x => x.GetByName(name2)).Returns(appSettingMaxUnfinishedAuctions);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(auctionMinStartPrice))
                .Returns(appSettingAuctionMinStartPrice);
            applicationSettingDataServiceMock.Setup(x => x.GetByName(maxOpenedAuctionsPerCategory))
                .Returns(appSettingMaxOpenedAuctionsPerCategory);

            productDataServiceMock.Setup(x => x.GetByID(productId)).Returns(new Product()
            { Id = productId, Categories = new HashSet<Category>() });
            auctionPlacingDataServiceMock.Setup(x => x.HasActiveAuctionPlacingRestrictions(userId)).Returns(false);

            userDataServiceMock.Setup(x => x.GetByID(userId)).Returns(new User() { Roles = roles });

            var service = new AuctionService(auctionDataServiceMock.Object, productDataServiceMock.Object,
                applicationSettingDataServiceMock.Object, auctionPlacingDataServiceMock.Object,
                userDataServiceMock.Object, categoryDataServiceMock.Object, loggerMock.Object);

            var exception = Record.Exception(() => service.Add(auction));

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