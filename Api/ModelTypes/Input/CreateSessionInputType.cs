using System;

namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to make a new session.
    /// </summary>
    public class CreateSessionInputType
    {
        /// <summary>
        /// The session's start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The session's end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The frequency of this session.
        /// How often the session occurs, in days.
        /// ie '7' for a week.
        /// </summary>
        public int? Frequency { get; set; }

        /// <summary>
        /// The session's venue.
        /// </summary>
        public string Venue { get; set; }

        /// <summary>
        /// The unique identifier of the league this session belongs to.
        /// </summary>
        public int LeagueId { get; set; }
    }
}
