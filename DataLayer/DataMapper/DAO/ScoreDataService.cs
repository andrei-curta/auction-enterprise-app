// <copyright file="ScoreDataService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace DataMapper.DAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    /// <summary>
    /// Implementation of the score data service.
    /// </summary>
    public class ScoreDataService : BaseRepository<Score>, IScoreDataService
    {
        /// <inheritdoc/>
        public virtual double CalculateUserScore(string userId, double defaultScore, uint? limit)
        {
            var scores = this.GetScoresAssignedToUser(userId, limit);

            // if there are no scores assigned by the user, return default score;
            if (scores.Count == 0)
            {
                return defaultScore;
            }
            else
            {
                return scores.Average(x => x.ScoreValue);
            }
        }

        /// <inheritdoc/>
        public virtual List<Score> GetScoresAssignedToUser(string userId, uint? limit)
        {
            using (var ctx = new AuctionEnterpriseContextFactory().CreateDbContext(new string[0]))
            {
                var results = ctx.Scores.Where(x => x.AssignedToUser.Id == userId).OrderByDescending(x => x.DateAdded)
                    .ToList();

                if (limit == null)
                {
                    return results;
                }
                else
                {
                    return results.Take((int)limit).ToList();
                }
            }
        }
    }
}