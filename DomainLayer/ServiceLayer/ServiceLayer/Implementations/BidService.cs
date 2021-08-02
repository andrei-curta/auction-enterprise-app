// <copyright file="BidService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using ServiceLayer.Interfaces;

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Implemantations;

    class BidService : BaseService<Bid, BidDataService, BidValidator>, IBidService
    {
        public BidService()
            : base(new BidDataService(), new BidValidator())
        {
        }
    }
}