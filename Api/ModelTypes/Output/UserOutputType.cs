using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a user.
    /// </summary>
    public class UserOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="UserOutputModel"/> to this output type.
        /// </summary>
        public static readonly Func<UserOutputModel, UserOutputType> FromModel
            = model => new UserOutputType { Model = model };

        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        public int Id => this.Model.Id;

        /// <summary>
        /// The user's registered email address.
        /// </summary>
        public string Email => this.Model.Email;

        /// <summary>
        /// The user's full name.
        /// </summary>
        public string Name => this.Model.Name;

        /// <summary>
        /// The date and time when the user signed up.
        /// </summary>
        public DateTime Joined => this.Model.Joined;

        /// <summary>
        /// The user's authorisation level.
        /// </summary>
        public string AuthLevel => this.Model.AuthLevel.ToString();

        internal UserOutputModel Model { get; set; }
    }
}
