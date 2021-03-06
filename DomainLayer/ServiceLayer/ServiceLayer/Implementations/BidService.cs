// <copyright file="BidService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using System;
    using System.Collections.Generic;
    using DataMapper.DAO;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using DomainModel.Validators;
    using FluentValidation;
    using Microsoft.Extensions.Logging;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Bid"/>.
    /// </summary>
    public class BidService : BaseService<Bid, BidDataService, BidValidator>, IBidService
    {
        private readonly IAuctionDataService auctionDataService;
        private readonly IUserDataService userDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        /// <param name="bidDataService">The bid data service.</param>
        /// <param name="auctionDataService">The auction data service.</param>
        /// <param name="userDataService">The user data service.</param>
        /// <param name="logger">The logger.</param>
        public BidService(
            BidDataService bidDataService,
            AuctionDataService auctionDataService,
            UserDataService userDataService,
            ILogger<BidService> logger = null)
            : base(bidDataService, new BidValidator(), logger)
        {
            this.auctionDataService = auctionDataService;
            this.userDataService = userDataService;
        }

        /// <inheritdoc/>
        public override void Add(Bid bid)
        {
            try
            {
                this.validator.ValidateAndThrow(bid);

                var auction = this.auctionDataService.GetByID(bid.AuctionId);

                if (auction == null)
                {
                    throw new ArgumentException("The auction was not found!");
                }

                if (auction.StartDate > DateTime.Now)
                {
                    throw new Exception("The auction has not started yet!");
                }

                // Pentru o licitatie incheiata nu se mai poate modifica nimic
                if (auction.Closed)
                {
                    throw new UnauthorizedAccessException("The auction is closed, you cannot add a bid to it");
                }

                // check if the bid is in the same currency as the auction.
                if (bid.BidValue.Currency != auction.StartPrice.Currency)
                {
                    throw new Exception("The bid must be placed in the same currency as the auction!");
                }

                // Pentru o licitare, pretul oferit la momentul n+1 trebuie sa fie strict mai mare decat pretul de la momentul n, dar cel mult cu 10% mai mult fata de pretul de la momentul n
                // if there are no bids, we consider the auction start price.
                var latestBid = this.service.GetLatestBidByAuction(auction.Id);
                var latestBidValue = auction.StartPrice;

                if (latestBid != null)
                {
                    latestBidValue = latestBid.BidValue;
                }

                if (latestBidValue.Amount >= bid.BidValue.Amount)
                {
                    throw new Exception("The amount must be greater than the one of the last bid!");
                }

                if (bid.BidValue.Amount > latestBidValue.Amount + (0.1M * latestBidValue.Amount))
                {
                    throw new Exception("You cannot bid with more than 10% more than the last bid!");
                }

                var biddingUser = this.userDataService.GetByID(bid.UserId);
                if (!biddingUser.IsInRole("Bidder"))
                {
                    throw new UnauthorizedAccessException("You are not in the role that allows bidding!");
                }

                this.service.Insert(bid);
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, e.Message);

                throw;
            }
        }

        /// <inheritdoc/>
        public override void Update(Bid entity)
        {
            try
            {
                throw new Exception("A bid once placed cannot be altered!");
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, e.Message);

                throw;
            }
        }
    }
}