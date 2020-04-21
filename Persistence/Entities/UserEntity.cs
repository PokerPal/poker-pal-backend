using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    /// <summary>
    /// The authorisation level of a user.
    /// </summary>
    public enum AuthLevel
    {
        /// <summary>
        /// Normal user authorisation level.
        /// </summary>
        User = 1,

        /// <summary>
        /// Administrator authorisation level.
        /// </summary>
        Admin,

        /// <summary>
        /// Super administrator authorisation level.
        /// </summary>
        SuperAdmin,
    }

    /// <summary>
    /// Represents a registered user in the database.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntity"/> class.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="email">The user's registered email address.</param>
        /// <param name="name">The user's full name.</param>
        /// <param name="joined">The date and time the user joined.</param>
        /// <param name="authLevel">The user's authorisation level.</param>
        /// <param name="passwordHash">The user's hashed password, together with its salt.</param>
        public UserEntity(
            int id,
            string email,
            string name,
            DateTime joined,
            AuthLevel authLevel,
            string passwordHash)
        {
            this.Id = id;
            this.Email = email;
            this.Name = name;
            this.Joined = joined;
            this.AuthLevel = authLevel;
            this.PasswordHash = passwordHash;
        }

        /// <summary>
        /// Gets or sets this user's unique identifier.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's registered email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time the user signed up.
        /// </summary>
        public DateTime Joined { get; set; }

        /// <summary>
        /// Gets or sets the user's authorisation level within the system.
        /// </summary>
        public AuthLevel AuthLevel { get; set; }

        /// <summary>
        /// Gets or sets the user's hashed (and salted) password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the user-session relations this user belongs to; autofilled when fetched
        /// from the database, null otherwise.
        /// </summary>
        public virtual IEnumerable<UserSessionEntity> UserSessions { get; set; } = null;

        /// <summary>
        /// Gets or sets the user-badge relations this user belongs to; autofilled when fetched
        /// from the database, null otherwise.
        /// </summary>
        public virtual IEnumerable<UserBadgeEntity> UserBadges { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of user leagues that have occurred in this league; autofilled when
        /// fetched from the database, null otherwise.
        /// </summary>
        public virtual IEnumerable<UserLeagueEntity> UserLeagues { get; set; } = null;
    }
}
