// <copyright file="BidService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

using DomainModel.ValueObjects;
using FluentValidation;

namespace ServiceLayer.Implementations
{
    using System;
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using Microsoft.Extensions.Logging;
    using ServiceLayer.Implementations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Bid"/>.
    /// </summary>
    public class BidService : BaseService<Bid, BidDataService, BidValidator>, IBidService
    {
        private readonly AuctionDataService auctionDataService = new AuctionDataService();

        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService(ILogger<BidService> logger = null)
            : base(new BidDataService(), new BidValidator(), logger)
        {
        }

        /// <inheritdoc/>
        public override void Add(Bid bid)
        {
            this.validator.ValidateAndThrow(bid);

            // Pentru o licitatie incheiata nu se mai poate modifica nimic
            var auction = this.auctionDataService.GetByID(bid.AuctionId);
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

            if (bid.BidValue.Amount + (0.1M * bid.BidValue.Amount) > latestBidValue.Amount)
            {
                throw new Exception("You cannot bid with more than 10% more than the last bid!");
            }

            this.service.Insert(bid);
        }

        public override void Update(Bid entity)
        {
            throw new Exception("A bid once placed cannot be altered!");
        }
    }
}