using System.Threading.Tasks;

using Application.Models.Output;
using Application.Models.Result;

using Microsoft.Extensions.Logging;

using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;

using Utility.ResultModel;

namespace Application.Services
{
    /// <summary>
    /// A service for performing operations on badges.
    /// </summary>
    public class BadgeService
    {
        private readonly ILogger<BadgeService> logger;
        private readonly IDatabaseContextFactory<DatabaseContext> databaseContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeService"/> class.
        /// </summary>
        /// <param name="databaseContextFactory">Factory for the database context.</param>
        /// <param name="logger">Logger for messages.</param>
        public BadgeService(
            ILogger<BadgeService> logger,
            IDatabaseContextFactory<DatabaseContext> databaseContextFactory)
        {
            this.logger = logger;
            this.databaseContextFactory = databaseContextFactory;
        }

        /// <summary>
        /// Get the details of a badge in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the badge.</param>
        /// <returns>The badge's information, if found.</returns>
        public async Task<Result<BadgeOutputModel, string>> GetBadgeAsync(int id)
        {
            using (this.logger.BeginScope($"Getting badge with id {id}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                return Result<BadgeEntity, string>
                    .FromNullableOr(
                        await context.Badges.FindAsync(id),
                        $"Badge not found.")
                    .OnErr(e => this.logger.LogWarning(e))
                    .Map(b => new BadgeOutputModel(
                        b.Id, b.Name, b.Description, b.Type));
            }
        }

        /// <summary>
        /// Create a new badge entity in the database.
        /// </summary>
        /// <param name="name">The badge's name.</param>
        /// <param name="description">The badge's description.</param>
        /// <param name="type">The type of the badge.</param>
        /// <returns>
        /// The result of creating the badge, if successful.
        /// </returns>
        public async Task<Result<CreateBadgeResultModel, string>> CreateBadge(
            string name,
            string description,
            string type)
        {
            using (this.logger.BeginScope($"Creating new badge with name {name}."))
            {
                await using var context = this.databaseContextFactory.CreateDatabaseContext();

                Result<BadgeType, string> badgeType = type switch
                {
                    "OptionA" => BadgeType.OptionA,
                    "OptionB" => BadgeType.OptionB,
                    _ => $"{type} is not a valid badge type.",
                };

                return await badgeType
                    .OnErr(e => this.logger.LogWarning(e))
                    .AndThenAsync<CreateBadgeResultModel>(async bt =>
                    {
                        var badge = new BadgeEntity(default, name, description, bt);

                        await context.Badges.AddAsync(badge);
                        await context.SaveChangesAsync();

                        return new CreateBadgeResultModel
                        {
                            Id = badge.Id,
                        };
                    });
            }
        }
    }
}
