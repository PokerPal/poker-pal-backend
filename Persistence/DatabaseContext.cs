using Microsoft.EntityFrameworkCore;

using Persistence.Entities;

namespace Persistence
{
    /// <inheritdoc />
    public partial class DatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuration of this context.</param>
        internal DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the set of all users in the database.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Gets or sets the set of all sessions in the database.
        /// </summary>
        public DbSet<SessionEntity> Sessions { get; set; }

        /// <summary>
        /// Gets or sets the set of all leagues in the database.
        /// </summary>
        public DbSet<LeagueEntity> Leagues { get; set; }

        /// <summary>
        /// Gets or sets the set of all badges in the database.
        /// </summary>
        public DbSet<BadgeEntity> Badges { get; set; }

        /// <summary>
        /// Gets or sets the set of all user-session link entities in the database.
        /// </summary>
        public DbSet<UserSessionEntity> UserSessions { get; set; }

        /// <summary>
        /// Gets or sets the set of all user-badge link entities in the database.
        /// </summary>
        public DbSet<UserBadgeEntity> UserBadges { get; set; }

        /// <summary>
        /// Gets or sets the set of all user-league link entities in the database.
        /// </summary>
        public DbSet<UserLeagueEntity> UserLeagues { get; set; }
    }
}
