namespace Persistence.Entities
{
    /// <summary>
    /// Represents a link between a user and a session in the database.
    /// </summary>
    public class UserSessionEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSessionEntity"/> class.
        /// </summary>
        /// <param name="userId">The unique identifier of the linked user.</param>
        /// <param name="sessionId">The unique identifier of the linked session.</param>
        /// <param name="totalScore">The total score of the user in this session.</param>
        public UserSessionEntity(int userId, int sessionId, int totalScore)
        {
            this.UserId = userId;
            this.SessionId = sessionId;
            this.TotalScore = totalScore;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the linked user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the linked session.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the total score of the user in this session.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// Gets or sets the users total score in the league this session is associated with.
        /// </summary>
        public int EndScore { get; set; }

        /// <summary>
        /// Gets or sets the user entity linked to this entity; autofilled when fetched from the
        /// database, null otherwise.
        /// </summary>
        public virtual UserEntity User { get; set; } = null;

        /// <summary>
        /// Gets or sets the session entity linked to this entity; autofilled when fetched from
        /// the database, null otherwise.
        /// </summary>
        public virtual SessionEntity Session { get; set; } = null;
    }
}
