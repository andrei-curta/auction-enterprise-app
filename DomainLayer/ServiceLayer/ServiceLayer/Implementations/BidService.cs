// <copyright file="BidService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
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
        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService(ILogger<BidService> logger = null)
            : base(new BidDataService(), new BidValidator(), logger)
        {
        }

        public override void Add(Bid entity)
        {
            //Pentru o licitare, pretul oferit la momentul n+1 trebuie sa fie strict mai mare decat pretul de la momentul n, dar cel mult cu 10% mai mult fata de pretul de la momentul n

            //Pentru o licitatie incheiata nu se mai poate modifica nimic

            //la inceputul licitatiei se va specifica o moneda de tranzactie si toate celelalte licitari pentru obiectul curent trebuie sa se faca in aceasta moneda
        }
    }
}