using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a user session.
    /// </summary>
    public class UserSessionOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="UserSessionOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<UserSessionOutputModel, UserSessionOutputType> FromModel
            = model => new UserSessionOutputType { Model = model };

        /// <summary>
        /// The users unique identifier.
        /// </summary>
        public int UserId => this.Model.UserId;

        /// <summary>
        /// The sessions unique identifier.
        /// </summary>
        public int SessionId => this.Model.SessionId;

        /// <summary>
        /// The user's total score in this session.
        /// </summary>
        public int TotalScore => this.Model.TotalScore;

        internal UserSessionOutputModel Model { get; set; }
    }
}