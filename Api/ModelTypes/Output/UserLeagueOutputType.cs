using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a user league.
    /// </summary>
    public class UserLeagueOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="UserLeagueOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<UserLeagueOutputModel, UserLeagueOutputType> FromModel
            = model => new UserLeagueOutputType { Model = model };

        /// <summary>
        /// The users unique identifier.
        /// </summary>
        public int UserId => this.Model.UserId;

        /// <summary>
        /// The leagues unique identifier.
        /// </summary>
        public int LeagueId => this.Model.LeagueId;

        /// <summary>
        /// The user's total score in this league.
        /// </summary>
        public int TotalScore => this.Model.TotalScore;

        /// <summary>
        /// The user's name.
        /// </summary>
        public string UserName => this.Model.UserName;

        internal UserLeagueOutputModel Model { get; set; }
    }
}