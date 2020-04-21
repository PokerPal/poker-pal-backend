using System;

using Persistence.Entities;

namespace Application.Models.Output
{
    /// <summary>
    /// Represents details about a user.
    /// </summary>
    public class UserOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserOutputModel"/> class.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="email">The user's registered email address.</param>
        /// <param name="name">The user's full name.</param>
        /// <param name="joined">The date and time when the user registered.</param>
        /// <param name="authLevel">The user's authorisation level.</param>
        public UserOutputModel(
            int id,
            string email,
            string name,
            DateTime joined,
            AuthLevel authLevel)
        {
            this.Id = id;
            this.Email = email;
            this.Name = name;
            this.Joined = joined;
            this.AuthLevel = authLevel;
        }

        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The user's registered email address.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The date and time when the user signed up.
        /// </summary>
        public DateTime Joined { get; }

        /// <summary>
        /// The user's authorisation level.
        /// </summary>
        public AuthLevel AuthLevel { get; }
    }
}
