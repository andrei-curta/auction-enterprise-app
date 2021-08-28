// <copyright file="IScoreDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Interface for the scoreDataService.
    /// </summary>
    public interface IScoreDataService : IRepository<Score>
    {
        /// <summary>
        /// Calculates the score of the user or returns the default value.
        /// </summary>
        /// <param name="userId">The Id uf the user to get the score of.</param>
        /// <param name="limit">The max number to return or all if null.</param>
        /// <returns>The score of the user in descending order by date added.</returns>
        public List<Score> GetScoresAssignedToUser(string userId, uint? limit);

        /// <summary>
        /// Calculates the score a user has.
        /// </summary>
        /// <param name="userId">The Id uf the user to get the score of.</param>
        /// <param name="defaultScore">If the user has not been assigned any score, this value will be returned.</param>
        /// <param name="limit">The max number to return or all if null.</param>
        /// <returns>The value of the score.</returns>
        public double CalculateUserScore(string userId, double defaultScore, uint? limit);
    }
}