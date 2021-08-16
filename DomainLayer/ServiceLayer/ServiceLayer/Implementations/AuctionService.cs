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
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Auction"/>.
    /// </summary>
    public class AuctionService : BaseService<Auction, AuctionDataService, AuctionValidator>
    {
        private readonly IApplicationSettingService applicationSettingService;

        private readonly IProductDataService productDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService(AuctionDataService auctionDataService, IProductDataService productDataService, ApplicationSettingDataService applicationSettingDataService)
            : base(auctionDataService, new AuctionValidator())
        {
            this.productDataService = productDataService;
            this.applicationSettingService = new ApplicationSettingService(applicationSettingDataService); 
        }

        /// <inheritdoc/>
        public override void Add(Auction entity)
        {
            this.validator.ValidateAndThrow(entity);

            if (this.HasReachedMaxNumberOfOpenedAuctions(entity.UserId))
            {
                throw new Exception("You have reached the max number of open auctions;");
            }

            var auctionedProduct = productDataService.GetByID(entity.ProductId);
            if (auctionedProduct == null)
            {
                throw new NullReferenceException("The Id for the product is invalid");
            }

            //todo: verificat daca produsul apartine userului curent
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

        /// <summary>
        /// Checks if the price is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>A boolean indicating whether the value for the auction is above the requiired value..</returns>
        public bool StartPriceIsAboveThreshold(decimal value)
        {
            decimal thresholdValue = this.applicationSettingService.GetValueAsDecimal("AuctionMinStartPrice");

            return value > thresholdValue;
        }
    }
}