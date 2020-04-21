using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a users streak.
    /// </summary>
    public class UserStreakOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="UserStreakOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<UserStreakOutputModel, UserStreakOutputType> FromModel
            = model => new UserStreakOutputType { Model = model };

        /// <summary>
        /// The users unique identifier.
        /// </summary>
        public int UserId => this.Model.UserId;

        /// <summary>
        /// The leagues unique identifier.
        /// </summary>
        public int LeagueId => this.Model.LeagueId;

        /// <summary>
        /// The streak the user is on.
        /// </summary>
        public int Streak => this.Model.Streak;

        /// <summary>
        /// The type of the streak.
        /// </summary>
        public string StreakType => this.Model.StreaksType.ToString();

        internal UserStreakOutputModel Model { get; set; }
    }
}