using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a session.
    /// </summary>
    public class UserLeagueHistoryOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="UserLeagueHistoryOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<UserLeagueHistoryOutputModel, UserLeagueHistoryOutputType>
            FromModel
                 = model => new UserLeagueHistoryOutputType { Model = model };

        /// <summary>
        /// The session's unique identifier.
        /// </summary>
        public int UserId => this.Model.UserId;

        /// <summary>
        /// The session's start date.
        /// </summary>
        public int LeagueId => this.Model.LeagueId;

        /// <summary>
        /// The session's end date.
        /// </summary>
        public int SessionId => this.Model.SessionId;

        /// <summary>
        /// The session frequency.
        /// </summary>
        public int TotalScore => this.Model.TotalScore;

        internal UserLeagueHistoryOutputModel Model { get; set; }
    }
}