// <copyright file="IAuctionService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>


namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>
    /// Interface for the auction Service.
    /// </summary>
    public interface IAuctionService : IService<Auction>
    {
        /// <summary>
        /// Allows the user that started the auction to close it before the expiration date.
        /// </summary>
        /// <param name="auction">The auction to be closed.</param>
        public void CancelAuction(Auction auction);
    }
}