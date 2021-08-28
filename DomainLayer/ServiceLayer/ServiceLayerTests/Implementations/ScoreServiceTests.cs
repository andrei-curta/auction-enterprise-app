using System;
using System.Collections.Generic;
using DataMapper.DAO;
using DomainModel.Models;
using FluentValidation;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class ScoreServiceTests
    {
        public static IEnumerable<object[]> TestAddCheckPlacingRestrictionData =>
            new List<object[]>
            {
                new object[]
                {
                    "2,5", "3", "3", 3.1, false, Times.Never()
                },
                new object[]
                {
                    "2,5", "3", "3", 3.00013211, false, Times.Never()
                },
                new object[]
                {
                    "2,5", "3", "3", 3.1, true, Times.Never()
                },
                new object[]
                {
                    "2", "3", "3", 1, false, Times.Once()
                },
                new object[]
                {
                    "2", "3", "3", 1, true, Times.Never()
                },
            };

        [Theory]
        [MemberData(nameof(TestAddCheckPlacingRestrictionData))]
        public void TestAddCheckPlacingRestriction(string auctionPlacingRestrictionsScore, string defaultScore, string topNScoresToConsider, double userScore, bool hasActiveRestrictions, Times times )
        {
            Mock<ScoreDataService> scoreDataServiceMock = new Mock<ScoreDataService>();
            Mock<AuctionPlacingRestrictionsDataService> auctionPlacingRestrictionsDataServiceMock =
                new Mock<AuctionPlacingRestrictionsDataService>();
            Mock<ApplicationSettingDataService> applicationSettingDataServiceMock =
                new Mock<ApplicationSettingDataService>();

            string userId = "1";

            var score = new Score()
            {
                AssignedToUser = new User(){Id = userId},
                AssignedByUser = new User(),
                ScoreValue = 2
            };

            auctionPlacingRestrictionsDataServiceMock.Setup(x => x.HasActiveAuctionPlacingRestrictions(userId))
                .Returns(hasActiveRestrictions);

            scoreDataServiceMock.Setup(x => x.CalculateUserScore(userId, Double.Parse(defaultScore), uint.Parse(topNScoresToConsider)))
                .Returns(userScore);

            applicationSettingDataServiceMock.Setup(x => x.GetByName("AuctionPlacingRestrictionsScore"))
                .Returns(new ApplicationSetting() { Value = auctionPlacingRestrictionsScore });
            applicationSettingDataServiceMock.Setup(x => x.GetByName("DefaultScore"))
                .Returns(new ApplicationSetting() { Value = defaultScore });
            applicationSettingDataServiceMock.Setup(x => x.GetByName("TopNScoresToConsider"))
                .Returns(new ApplicationSetting() { Value = topNScoresToConsider });
            applicationSettingDataServiceMock.Setup(x => x.GetByName("NumberOfRestrictionDays"))
                .Returns(new ApplicationSetting() { Value = "7" });

            var service = new ScoreService(scoreDataServiceMock.Object,
                auctionPlacingRestrictionsDataServiceMock.Object, applicationSettingDataServiceMock.Object);

            service.Add(score);

            // Explicitly verify each expectation...
            auctionPlacingRestrictionsDataServiceMock.Verify(mock => mock.Insert(It.IsAny < AuctionPlacingRestriction>()), times);
        }
    }
}