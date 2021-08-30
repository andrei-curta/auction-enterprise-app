// <copyright file="AuctionService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using System;
    using System.Linq;
    using DataMapper.DAO;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using DomainModel.Validators;
    using FluentValidation;
    using ServiceLayer.Implementations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Auction"/>.
    /// </summary>
    public class AuctionService : BaseService<Auction, AuctionDataService, AuctionValidator>, IAuctionService
    {
        private readonly IApplicationSettingService applicationSettingService;

        private readonly IProductDataService productDataService;

        private readonly IAuctionPlacingRestrictionsDataService auctionPlacingRestrictionsDataService;

        private readonly IUserDataService userDataService;

        private readonly ICategoryDataService categoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        /// <param name="auctionDataService">The auction data service.</param>
        /// <param name="productDataService">The product data service.</param>
        /// <param name="applicationSettingDataService">The application setting data service.</param>
        /// <param name="auctionPlacingRestrictionsDataService">The auction placing restrictions data service.</param>
        /// <param name="userDataService">The user data service.</param>
        /// <param name="categoryDataService">The category data service.</param>
        public AuctionService(
            AuctionDataService auctionDataService,
            IProductDataService productDataService,
            ApplicationSettingDataService applicationSettingDataService,
            AuctionPlacingRestrictionsDataService auctionPlacingRestrictionsDataService,
            UserDataService userDataService,
            CategoryDataService categoryDataService)
            : base(auctionDataService, new AuctionValidator())
        {
            this.productDataService = productDataService;
            this.applicationSettingService = new ApplicationSettingService(applicationSettingDataService);
            this.auctionPlacingRestrictionsDataService = auctionPlacingRestrictionsDataService;
            this.userDataService = userDataService;
            this.categoryDataService = categoryDataService;
        }

        /// <inheritdoc/>
        public override void Add(Auction entity)
        {
            this.validator.ValidateAndThrow(entity);

            int maxAuctionDurationMonths = this.applicationSettingService.GetValueAsInt("AuctionMaxDurationMonths");
            if (entity.EndDate.Subtract(entity.StartDate).Days / (365.25 / 12) > maxAuctionDurationMonths)
            {
                throw new ArgumentException("The max duration of an auction was exceded.");
            }

            if (this.HasReachedMaxNumberOfOpenedAuctions(entity.UserId))
            {
                throw new Exception("You have reached the max number of open auctions;");
            }

            decimal thresholdValue = this.applicationSettingService.GetValueAsDecimal("AuctionMinStartPrice");
            if (entity.StartPrice.Amount < thresholdValue)
            {
                throw new Exception($"The price set is below the threshold of {thresholdValue}");
            }

            bool hasAuctionPlacingRestrictions =
                this.auctionPlacingRestrictionsDataService.HasActiveAuctionPlacingRestrictions(entity.UserId);

            if (hasAuctionPlacingRestrictions)
            {
                throw new Exception("You have an active auction placing restriction. Try later.");
            }

            var user = this.userDataService.GetByID(entity.UserId);
            if (!user.IsInRole("AUCTIONER"))
            {
                throw new UnauthorizedAccessException("You do not have the necessary role to add an auction!");
            }

            var maxOpenedAuctionsPerCategory =
                this.applicationSettingService.GetValueAsInt("MaxOpenedAuctionsPerCategory");

            var auctionedProduct = this.productDataService.GetByID(entity.ProductId);

            var categoriesWithOpenAuctions = this.categoryDataService.GetNumberOfOpenedAuctionsByCategory(entity.UserId);

            foreach (var category in auctionedProduct.Categories)
            {
                if (categoriesWithOpenAuctions.ContainsKey(category) && categoriesWithOpenAuctions[category] > maxOpenedAuctionsPerCategory)
                {
                    throw new Exception("Too many opened auctions in category!");
                }
            }

            this.service.Insert(entity);
        }

        /// <summary>
        /// Checks if the user has surpassed the max number of open auctions.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A boolean indicating whether the max number of open auctions was reached.</returns>
        public bool HasReachedMaxNumberOfOpenedAuctions(string userId)
        {
            var userAuctions = this.service.GetAuctionsByUserId(userId);
            int unfinishedAuctions = userAuctions.Count(x => !x.Closed);

            int maxUnfinishedAuctions = this.applicationSettingService.GetValueAsInt("MaxUnfinishedAuctions");

            return unfinishedAuctions > maxUnfinishedAuctions;
        }

        /// <inheritdoc/>
        public override void Update(Auction auction)
        {
            var dbAuction = this.service.GetByID(auction.Id);

            if (dbAuction.Closed)
            {
                throw new UnauthorizedAccessException("The auction is closed, you cannot update it!");
            }
            else
            {
                this.service.Update(auction);
            }
        }
    }
}