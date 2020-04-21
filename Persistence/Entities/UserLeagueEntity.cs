namespace Persistence.Entities
{
    /// <summary>
    /// The users status in a league.
    /// </summary>
    public class UserLeagueEntity
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="UserLeagueEntity"/> class.
        /// </summary>
        /// <param name="userId">The users unique identification.</param>
        /// <param name="leagueId">The leagues unique identification.</param>
        /// <param name="totalScore">The users total score in the league.</param>
        public UserLeagueEntity(int userId, int leagueId, int totalScore)
        {
            this.UserId = userId;
            this.LeagueId = leagueId;
            this.TotalScore = totalScore;
        }

        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the League ID.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the total score.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// Gets or sets the user entity linked to this entity; autofilled when fetched from the
        /// database, null otherwise.
        /// </summary>
        public virtual UserEntity User { get; set; } = null;

        /// <summary>
        /// Gets or sets the league this session belongs to; autofilled when fetched from the
        /// database, null otherwise.
        /// </summary>
        public virtual LeagueEntity League { get; set; } = null;
    }
}