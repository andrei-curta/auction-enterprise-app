// <copyright file="BidService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


using System.Collections.Generic;

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using Microsoft.Extensions.Logging;
    using ServiceLayer.Implemantations;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Bid"/>.
    /// </summary>
    public class BidService : BaseService<Bid, BidDataService, BidValidator>, IBidService
    {
        private readonly ILogger<BidService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService(ILogger<BidService> logger = null)
            : base(new BidDataService(), new BidValidator())
        {
            this.logger = logger;
        }

        public override Bid GetById(long id)
        {
            logger.Log(LogLevel.Warning, "asdasdasdasd asd asdas ad LOG");
            return new Bid();
        }
    }
}