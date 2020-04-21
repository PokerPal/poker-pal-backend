using System;

namespace Application.Models.Output
{
    /// <summary>
    /// Represents details about a session.
    /// </summary>
    public class SessionOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionOutputModel"/> class.
        /// </summary>
        /// <param name="id">The session's unique identifier.</param>
        /// <param name="startDate">The start date of the session.</param>
        /// <param name="endDate">The end date of the session.</param>
        /// <param name="frequency">How often the session occurs.</param>
        /// <param name="venue">Where the session takes place.</param>
        /// <param name="finalized">Whether or not the session has been finalized.</param>
        public SessionOutputModel(
            int id,
            DateTime startDate,
            DateTime endDate,
            int? frequency,
            string venue,
            bool finalized)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Frequency = frequency;
            this.Venue = venue;
            this.Finalized = finalized;
        }

        /// <summary>
        /// The session's unique identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The session's start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// The session's end date.
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// The frequency of this session.
        /// </summary>
        public int? Frequency { get; }

        /// <summary>
        /// The session's venue.
        /// </summary>
        public string Venue { get; }

        /// <summary>
        /// Whether or not the session has been finalized.
        /// </summary>
        public bool Finalized { get; }
    }
}
