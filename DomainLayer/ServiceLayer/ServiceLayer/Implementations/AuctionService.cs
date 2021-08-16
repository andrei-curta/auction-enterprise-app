// <copyright file="AuctionService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using DataMapper.Interfaces;
using FluentValidation;

namespace ServiceLayer.Implementations
{
    using System;
    using System.Linq;
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Auction"/>.
    /// </summary>
    public class AuctionService : BaseService<Auction, AuctionDataService, AuctionValidator>
    {
        private readonly IApplicationSettingService applicationSettingService = new ApplicationSettingService(new ApplicationSettingDataService());
        private readonly IProductDataService productDataService = new ProductDataService();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
            : base(new AuctionDataService(), new AuctionValidator())
        {
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
        private bool HasReachedMaxNumberOfOpenedAuctions(string userId)
        {
            var userAuctions = this.service.GetAuctionsByUserId(userId);
            int unfinishedAuctions = userAuctions.Count(x => !x.Closed);

            int maxUnfinishedAuctions = this.applicationSettingService.GetValueAsInt("MaxUnfinishedAuctions");

            return unfinishedAuctions > maxUnfinishedAuctions;
        }

        private bool StartPriceIsAboveThreshold(decimal value)
        {
            decimal thresholdValue = this.applicationSettingService.GetValueAsDecimal("AuctionMinStartPrice");

            return value > thresholdValue;
        }
    }
}