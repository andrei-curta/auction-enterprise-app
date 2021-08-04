// <copyright file="AuctionService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;

    /// <summary>
    /// Provides services regarding the <see cref="Auction"/>.
    /// </summary>
    public class AuctionService : BaseService<Auction, AuctionDataService, AuctionValidator>
    {
        public AuctionService()
            : base(new AuctionDataService(), new AuctionValidator())
        {
        }

        public override void Add(Auction entity)
        {


            base.Add(entity);
        }
    }
}