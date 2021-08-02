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

    /// <summary>
    /// Provides services regarding the <see cref="Bid"/>.
    /// </summary>
    public class BidService : BaseService<Bid, BidDataService, BidValidator>, IBidService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService()
            : base(new BidDataService(), new BidValidator())
        {
        }
    }
}