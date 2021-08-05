// <copyright file="AuctionService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>



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
        private readonly IApplicationSettingService applicationSettingService = new ApplicationSettingService();

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
            var userAuctions = this.service.GetAuctionsByUserId(entity.UserId);
            int unfinishedAuctions = userAuctions.Count(x => !x.Closed);

            int maxUnfinishedAuctions = int.Parse(this.applicationSettingService.GetByName("MaxUnfinishedAuctions").Value);

            if (unfinishedAuctions > maxUnfinishedAuctions)
            {
                throw new Exception("You have reached the max number of open auctions;");
            }

            base.Add(entity);
        }
    }
}