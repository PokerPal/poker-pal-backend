using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Application.Cryptography.Services;
using Application.Models.Output;
using Application.Models.Result;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;

using Utility.ResultModel;

namespace Application.Services
{
    /// <summary>
    /// Service for performing operations on users.
    /// </summary>
    public class UserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IDatabaseContextFactory<DatabaseContext> databaseContextFactory;
        private readonly CryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="logger">Logger for messages.</param>
        /// <param name="databaseContextFactory">Factory for the database context.</param>
        /// <param name="cryptoService">The cryptography service.</param>
        public UserService(
            ILogger<UserService> logger,
            IDatabaseContextFactory<DatabaseContext> databaseContextFactory,
            CryptoService cryptoService)
        {
            this.logger = logger;
            this.databaseContextFactory = databaseContextFactory;
            this.cryptoService = cryptoService;
        }

        /// <summary>
        /// Get the details of a user in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user's details, if found.</returns>
        public async Task<Result<UserOutputModel, string>> GetUserAsync(int id)
        {
            using (this.logger.BeginScope($"Getting user with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                return Result<UserEntity, string>
                    .FromNullableOr(
                        await context.Users.FindAsync(id),
                        $"User not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(u => new UserOutputModel(
                        u.Id, u.Email, u.Name, u.Joined, u.AuthLevel));
            }
        }

        /// <summary>
        /// Get the details of a users streak from the database.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="leagueId">The unique identifier of the league.</param>
        /// <returns>The user's streak details, if found.</returns>
        public async Task<Result<UserStreakOutputModel, string>> GetUserStreakAsync(int userId, int leagueId)
        {
            using (this.logger.BeginScope($"Getting users streak with id {userId} within the league {leagueId}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();
                var league = await context.Leagues
                    .Include(l => l.Sessions)
                    .Where(l => l.Id == leagueId)
                    .FirstOrDefaultAsync();
                if (league == null)
                {
                    this.logger.LogWarning($"league with id {leagueId} not found.");
                    return $"League with id {leagueId} not found.";
                }

                if (await context.Users.FindAsync(userId) == null)
                {
                    this.logger.LogWarning($"User with id {userId} not found.");
                    return $"User with id {userId} not found.";
                }

                var streak = 0;
                var streakType = StreakType.Loss;
                foreach (var session in league.Sessions.OrderByDescending(s => s.StartDate))
                {
                    var userSession = await context.UserSessions
                        .FirstOrDefaultAsync(us =>
                            us.UserId == userId &&
                            us.SessionId == session.Id);
                    if (userSession != null)
                    {
                        var currentScore = league.Type == LeagueType.Cash ? userSession.TotalScore : userSession.EndScore;
                        if (streak == 0)
                        {
                            streakType = currentScore > 0 ? StreakType.Win : StreakType.Loss;
                        }

                        if ((currentScore > 0 && streakType == StreakType.Win)
                            || (currentScore <= 0 && streakType == StreakType.Loss))
                        {
                            streak += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                return new UserStreakOutputModel(userId, leagueId, streak, streakType);
            }
        }

        /// <summary>
        /// Attempt to log in with the provided email and password, returning an error if incorrect
        /// or user not found.
        /// </summary>
        /// <param name="email">The email provided by the user.</param>
        /// <param name="password">The password provided by the user.</param>
        /// <returns>The details of the user, if credentials were correct.</returns>
        public async Task<Result<LogInResultModel, string>> VerifyUserAsync(
            string email,
            string password)
        {
            using (this.logger.BeginScope($"Verifying provided credentials for {email}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                return Result<UserEntity, string>
                    .FromNullableOr(
                        await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync(),
                        "User not found or password incorrect.")
                    .OnErr(e => this.logger.LogWarning(
                        $"User with email {email} not found."))
                    .AndThen<LogInResultModel>(user =>
                    {
                        var salt = this.cryptoService.DecompressSalt(user.PasswordHash);

                        var testHash = this.cryptoService.CalculateHash(password, salt);
                        var testSalted = this.cryptoService.CompressHash(salt, testHash);

                        if (testSalted == user.PasswordHash)
                        {
                            return new LogInResultModel
                            {
                                Id = user.Id,
                                Email = user.Email,
                            };
                        }

                        this.logger.LogWarning(
                            $"Incorrect password provided for user with email {email}");
                        return "User not found or password incorrect.";
                    });
            }
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The outcome of the deletion.</returns>
        public async Task<Result<DeleteUserResultModel, string>> DeleteUserAsync(int id)
        {
            using (this.logger.BeginScope($"Deleting user with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return Result
                        .Err($"User not found.")
                        .OnErr(e => this.logger.LogWarning(e));
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return new DeleteUserResultModel()
                {
                    Id = user.Id,
                };
            }
        }

        /// <summary>
        /// Get the details of all users in the database.
        /// </summary>
        /// <returns>The user's details, if found.</returns>
        public async Task<Result<IEnumerable<UserOutputModel>, string>> GetAllUsersAsync()
        {
            using (this.logger.BeginScope("Getting list of all users."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                return context.Users
                    .Select(user => new UserOutputModel(
                        user.Id,
                        user.Email,
                        user.Name,
                        user.Joined,
                        user.AuthLevel))
                    .ToList();
            }
        }

        /// <summary>
        /// Search for users whose names or emails contain the given query string.
        /// </summary>
        /// <param name="query">The string to search for in users' names and emails.</param>
        /// <returns>The list of matching users.</returns>
        public async Task<Result<IEnumerable<UserOutputModel>, string>> SearchUsersAsync(
            string query)
        {
            using (this.logger.BeginScope($"Searching users with query {query}"))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var queryUpper = query.ToUpper();
                this.logger.LogInformation($"Processed query string: {queryUpper}");

                return context.Users
                    .Where(u =>
                        u.Name.ToUpper().Contains(queryUpper) ||
                        u.Email.ToUpper().Contains(queryUpper))
                    .Select(u => new UserOutputModel(
                        u.Id, u.Email, u.Name, u.Joined, u.AuthLevel))
                    .ToList();
            }
        }

        /// <summary>
        /// Create a new user entity in the database.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="name">The new user's full name.</param>
        /// <param name="password">The user's chosen password.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
        /// </returns>
        public async Task<Result<CreateUserResultModel, string>> CreateUserAsync(
            string email,
            string name,
            string password)
        {
            using (this.logger.BeginScope($"Creating new user with name {name}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var salt = this.cryptoService.GenerateSalt();
                var hash = this.cryptoService.CalculateHash(password, salt);
                var compressedHash = this.cryptoService.CompressHash(salt, hash);

                var user = new UserEntity(
                    default,
                    email,
                    name,
                    DateTime.Now,
                    AuthLevel.User,
                    compressedHash);

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return new CreateUserResultModel
                {
                    Id = user.Id,
                };
            }
        }

        /// <summary>
        /// Get all of a user's sessions.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>All of the user's sessions.</returns>
        public async Task<Result<IEnumerable<SessionOutputModel>, string>> GetUserSessions(int id)
        {
            using (this.logger.BeginScope($"Getting sessions for user with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var user = await context.Users
                    .Include(u => u.UserSessions)
                    .ThenInclude(us => us.Session)
                    .SingleOrDefaultAsync(u => u.Id == id);

                return Result<UserEntity, string>
                    .FromNullableOr(user, "User not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(u => u.UserSessions
                        .Select(us => us.Session)
                        .Select(s => new SessionOutputModel(
                            s.Id, s.StartDate, s.EndDate, s.Frequency, s.Venue, s.Finalized)));
            }
        }

        /// <summary>
        /// Get all of a user's badges.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>All of the user's badges.</returns>
        public async Task<Result<IEnumerable<BadgeOutputModel>, string>> GetUserBadges(int id)
        {
            using (this.logger.BeginScope($"Getting badges for user with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                var user = await context.Users
                    .Include(u => u.UserBadges)
                    .ThenInclude(ub => ub.Badge)
                    .SingleOrDefaultAsync(u => u.Id == id);

                return Result<UserEntity, string>
                    .FromNullableOr(user, "User not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(u => u.UserBadges
                        .Select(ub => ub.Badge)
                        .Select(b => new BadgeOutputModel(
                            b.Id, b.Name, b.Description, b.Type)));
            }
        }

        /// <summary>
        /// Create a link between a given user and a given badge.
        /// </summary>
        /// <param name="userId">The userID to link.</param>
        /// <param name="badgeId">The badgeID to link.</param>
        /// <returns>The outcome of the operation.</returns>
        public async Task<Result<CreateUserBadgeResultModel, string>> AddBadge(
            int userId,
            int badgeId)
        {
            using (this.logger.BeginScope(
                $"Giving badge with id {badgeId} to user with id {userId}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                if (await context.Users.FindAsync(userId) == null)
                {
                    return Result
                        .Err($"User not found.")
                        .OnErr(e => this.logger.LogWarning(e));
                }

                if (await context.Badges.FindAsync(badgeId) == null)
                {
                    return Result
                        .Err($"Badge not found.")
                        .OnErr(e => this.logger.LogWarning(e));
                }

                if (await context.UserBadges.FindAsync(userId, badgeId) != null)
                {
                    return Result
                        .Err($"User already has badge with id {badgeId}.")
                        .OnErr(e => this.logger.LogWarning(e));
                }

                await context.UserBadges.AddAsync(new UserBadgeEntity(userId, badgeId));
                await context.SaveChangesAsync();

                return new CreateUserBadgeResultModel
                {
                    BadgeId = badgeId,
                    UserId = userId,
                };
            }
        }
    }
}
