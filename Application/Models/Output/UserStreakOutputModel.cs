namespace Application.Models.Output
{
        /// <summary>
        /// The type of streak the user is on, winning or losing.
        /// </summary>
    public enum StreakType
    {
        /// <summary>
        /// The users streak is a winning streak.
        /// </summary>
        Win = 1,

        /// <summary>
        /// The users streak is a losing streak.
        /// </summary>
        Loss = 2,
    }

    /// <summary>
    /// The users current winning or losing streak.
    /// </summary>
    public class UserStreakOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStreakOutputModel"/> class.
        /// </summary>
        /// <param name="userId">The users unique id.</param>
        /// <param name="leagueId">The leagues unique id.</param>
        /// <param name="streak">The users total streak for the league.</param>
        /// <param name="streakType">The type of streak the user is on.</param>
        public UserStreakOutputModel(int userId, int leagueId, int streak, StreakType streakType)
        {
            this.UserId = userId;
            this.LeagueId = leagueId;
            this.Streak = streak;
            this.StreaksType = streakType;
        }

        /// <summary>
        /// The unique identifier of the linked user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The unique identifier of the linked league.
        /// </summary>
        public int LeagueId { get; }

        /// <summary>
        /// The streak the user is on.
        /// </summary>
        public int Streak { get; }

        /// <summary>
        /// The type of the streak.
        /// </summary>
        public StreakType StreaksType { get; }
        }
}