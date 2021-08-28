// <copyright file="ScoreService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using System;
    using DataMapper.DAO;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Score"/>.
    /// </summary>
    public class ScoreService : BaseService<Score, ScoreDataService, ScoreValidator>, IScoreService
    {
        private readonly IAuctionPlacingRestrictionsDataService auctionPlacingRestrictionsDataService;
        private readonly IApplicationSettingService applicationSettingService;

        public ScoreService(ScoreDataService scoreDataService,
            AuctionPlacingRestrictionsDataService auctionPlacingRestrictionsDataService,
            ApplicationSettingService applicationSettingService)
            : base(scoreDataService, new ScoreValidator())
        {
            this.auctionPlacingRestrictionsDataService = auctionPlacingRestrictionsDataService;
            this.applicationSettingService = applicationSettingService;
        }

        /// <inheritdoc/>
        public override void Add(Score score)
        {
            base.Add(score);

            // get the app settings
            var auctionPlacingRestrictionsScore =
                this.applicationSettingService.GetValueAsDecimal("AuctionPlacingRestrictionsScore");
            var defaultScore = this.applicationSettingService.GetValueAsDecimal("DefaultScore");
            var topNScores = this.applicationSettingService.GetValueAsInt("TopNScoresToConsider");
            var numberOfRestrictionDays = this.applicationSettingService.GetValueAsInt("NumberOfRestrictionDays");

            var newScore =
                this.service.CalculateUserScore(score.AssignedToUser.Id, (double)defaultScore, (uint)topNScores);

            // if the score is below the limit -> restrictions
            bool hasRestrictions =
                this.auctionPlacingRestrictionsDataService.HasActiveAuctionPlacingRestrictions(score.AssignedToUser.Id);

            if (!hasRestrictions)
            {
                var restriction = new AuctionPlacingRestriction()
                {
                    User = score.AssignedToUser,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(numberOfRestrictionDays),
                };

                this.auctionPlacingRestrictionsDataService.Insert(restriction);
            }
        }
    }
}