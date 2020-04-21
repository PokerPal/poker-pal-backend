using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a session.
    /// </summary>
    public class SessionOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="SessionOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<SessionOutputModel, SessionOutputType> FromModel
            = model => new SessionOutputType { Model = model };

        /// <summary>
        /// The session's unique identifier.
        /// </summary>
        public int Id => this.Model.Id;

        /// <summary>
        /// The session's start date.
        /// </summary>
        public DateTime StartDate => this.Model.StartDate;

        /// <summary>
        /// The session's end date.
        /// </summary>
        public DateTime EndDate => this.Model.EndDate;

        /// <summary>
        /// The session frequency.
        /// </summary>
        public int? Frequency => this.Model.Frequency;

        /// <summary>
        /// The session's venue.
        /// </summary>
        public string Venue => this.Model.Venue;

        /// <summary>
        /// Whether or not the session has been finalized.
        /// </summary>
        public bool Finalized => this.Model.Finalized;

        internal SessionOutputModel Model { get; set; }
    }
}
