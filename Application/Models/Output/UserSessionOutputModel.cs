namespace Application.Models.Output
{
    /// <summary>
    /// Represents details of a user session.
    /// </summary>
    public class UserSessionOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSessionOutputModel"/> class.
        /// </summary>
        /// <param name="userId">The users unique id.</param>
        /// <param name="sessionId">The sessions unique id.</param>
        /// <param name="totalScore">The users total score for the session.</param>
        public UserSessionOutputModel(int userId, int sessionId, int totalScore)
        {
            this.UserId = userId;
            this.SessionId = sessionId;
            this.TotalScore = totalScore;
        }

        /// <summary>
        /// The unique identifier of the linked user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The unique identifier of the linked session.
        /// </summary>
        public int SessionId { get; }

        /// <summary>
        /// The total score of the user in this session.
        /// </summary>
        public int TotalScore { get; }
    }
}