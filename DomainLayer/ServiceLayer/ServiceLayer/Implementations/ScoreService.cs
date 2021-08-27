// <copyright file="ScoreService.cs" company="Transilvania University of Brașov">
// Copyright (c) Curta Andrei. All rights reserved.
// </copyright>

namespace ServiceLayer.Implementations
{
    using DataMapper.DAO;
    using DomainModel.Models;
    using DomainModel.Validators;
    using ServiceLayer.Interfaces;

    /// <summary>
    /// Provides services regarding the <see cref="Score"/>.
    /// </summary>
    public class ScoreService : BaseService<Score, ScoreDataService, ScoreValidator>, IScoreService
    {
        public ScoreService(ScoreDataService scoreDataService)
            : base(scoreDataService, new ScoreValidator())
        {
        }
    }
}
