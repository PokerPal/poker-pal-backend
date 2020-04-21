using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Models.Output;
using Application.Models.Result;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;

using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;

using Utility.ResultModel;

namespace Application.Services
{
    /// <summary>
    /// A service for performing operations on leagues.
    /// </summary>
    public class LeagueService
    {
        private readonly ILogger<LeagueService> logger;
        private readonly IDatabaseContextFactory<DatabaseContext> databaseContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueService"/> class.
        /// </summary>
        /// <param name="databaseContextFactory">Factory for the database context.</param>
        /// <param name="logger">Logger for messages.</param>
        public LeagueService(
            ILogger<LeagueService> logger,
            IDatabaseContextFactory<DatabaseContext> databaseContextFactory)
        {
            this.logger = logger;
            this.databaseContextFactory = databaseContextFactory;
        }

        /// <summary>
        /// Get the details of a league in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the league.</param>
        /// <returns>The league's information, if found.</returns>
        public async Task<Result<LeagueOutputModel, string>> GetLeagueAsync(int id)
        {
            using (this.logger.BeginScope($"Getting league with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                return Result<LeagueEntity, string>
                    .FromNullableOr(
                        await context.Leagues.FindAsync(id),
                        $"League with id {id} not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(t => new LeagueOutputModel(
                        t.Id, t.Name, t.StartingAmount, t.AllowChanges, t.Type));
            }
        }

        /// <summary>
        /// Get all the user leagues within a league.
        /// </summary>
        /// <param name="id">The leagues id.</param>
        /// <returns>All of the user leagues associated with a league.</returns>
        public async Task<Result<IEnumerable<UserLeagueOutputModel>, string>> GetUserLeagues(int id)
        {
            using (this.logger.BeginScope($"Getting user leagues associated with league: {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var league = await context.Leagues
                    .Include(l => l.UserLeagues)
                    .ThenInclude(ul => ul.User)
                    .SingleOrDefaultAsync(l => l.Id == id);

                return Result<LeagueEntity, string>
                    .FromNullableOr(league, $"League with ID {id} not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(l => l.UserLeagues
                        .Select(ul =>
                            new UserLeagueOutputModel(ul.UserId, ul.LeagueId, ul.TotalScore, ul.User.Name))
                        .OrderByDescending(ul => ul.TotalScore).AsEnumerable())
                    .OnErr(e => this.logger.LogWarning(e));
            }
        }

        /// <summary>
        /// Get all the user sessions within a session.
        /// </summary>
        /// <param name="leagueId">The league unique Id.</param>
        /// <param name="userId">The user unique Id.</param>
        /// <returns>All of user sessions associated with a session.</returns>
        public async Task<Result<IEnumerable<UserLeagueHistoryOutputModel>, string>>
            GetUserLeagueHistory(int leagueId, int userId)
        {
            using (this.logger.BeginScope(
                $"Getting user {userId} history associated with league: {leagueId}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();
                var userSessions = context.UserSessions.Where(
                    us => us.UserId.Equals(userId) && us.Session.LeagueId.Equals(leagueId));
                var userLeagueHistoryOutputModels = userSessions
                    .Select(us => new UserLeagueHistoryOutputModel(
                        userId, leagueId, us.SessionId, us.EndScore))
                    .ToList();

                return userLeagueHistoryOutputModels;
            }
        }

        /// <summary>
        /// Get the details of a user league in the database.
        /// </summary>
        /// <param name="leagueId">The leagues unique id.</param>
        /// <param name="userId">The users unique id.</param>
        /// <param name="places">The amount of users above and below the current user to include.</param>
        /// <returns>The user league's information, if found along with the context.</returns>
        public async Task<Result<IEnumerable<UserLeagueOutputModel>, string>> GetUserLeague(
            int leagueId, int userId, int places)
        {
            using (this.logger.BeginScope($"Getting user league with league id {leagueId} and user Id {userId}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();
                var user = await context.Users.FindAsync(userId);
                if (user == null)
                {
                    this.logger.LogWarning($"user with it {userId} not found");
                    return "user not found";
                }

                var league = await context.Leagues
                    .Include(l => l.UserLeagues)
                    .ThenInclude(ul => ul.User)
                    .SingleOrDefaultAsync(l => l.Id == leagueId);

                var userLeague = await context.UserLeagues.FindAsync(userId, leagueId);

                if (userLeague == null)
                {
                    this.logger.LogWarning($"no user league found for user{userId} within league {leagueId}");
                    return "no user league found";
                }

                var aboveUserLeague = context.UserLeagues.OrderBy(ul => ul.TotalScore)
                    .Where(ul => ul.TotalScore > userLeague.TotalScore && ul.LeagueId == leagueId)
                    .ToList();

                aboveUserLeague = aboveUserLeague.GetRange(0, Math.Min(places, aboveUserLeague.Count));

                var underUserLeague = context.UserLeagues.OrderByDescending(ul => ul.TotalScore)
                    .Where(ul => ul.TotalScore <= userLeague.TotalScore && ul.LeagueId == leagueId).ToList();

                underUserLeague = underUserLeague.GetRange(0, Math.Min(places + 1, underUserLeague.Count));

                IEnumerable<UserLeagueEntity> userLeagueEntities = aboveUserLeague.Concat(underUserLeague).OrderByDescending(ul =>
                    ul.TotalScore);

                return userLeagueEntities.Select(ul => new UserLeagueOutputModel(ul.UserId, ul.LeagueId, ul.TotalScore, ul.User.Name)).ToList();
            }
        }

        /// <summary>
        /// Create a new league entity in the database.
        /// </summary>
        /// <param name="name">The name of the league.</param>
        /// <param name="startingAmount">The starting amount for users in the league.</param>
        /// <param name="allowChanges">Whether or not entities linked to this league can be changed.</param>
        /// <param name="type">The type of the league, if it is cash based or point based.</param>
        /// <returns>The result of the operation.</returns>
        public async Task<Result<CreateLeagueResultModel, string>> CreateLeague(
            string name, int startingAmount, bool allowChanges, LeagueType type)
        {
            using (this.logger.BeginScope($"Creating new league \"{name}\"."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var league = new LeagueEntity(default, name, startingAmount, allowChanges, type);
                await context.Leagues.AddAsync(league);
                await context.SaveChangesAsync();

                return new CreateLeagueResultModel
                {
                    Id = league.Id,
                };
            }
        }
    }
}
