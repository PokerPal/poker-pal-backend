using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    /// <summary>
    /// Represents a session in the database.
    /// </summary>
    public class SessionEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionEntity"/> class.
        /// </summary>
        /// <param name="id">The session's unique identifier.</param>
        /// <param name="startDate">The session's start date.</param>
        /// <param name="endDate">The session's end date.</param>
        /// <param name="frequency">The session's frequency.</param>
        /// <param name="venue">The venue of the session.</param>
        /// <param name="leagueId">The ID of the league this session belongs to.</param>
        public SessionEntity(
            int id,
            DateTime startDate,
            DateTime endDate,
            int? frequency,
            string venue,
            int leagueId)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Frequency = frequency;
            this.Venue = venue;
            this.LeagueId = leagueId;
            this.Finalized = false;
        }

        /// <summary>
        /// Gets or sets this session's unique identifier.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this session's start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets this session's end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the frequency of this session.
        /// </summary>
        public int? Frequency { get; set; }

        /// <summary>
        /// Gets or sets this session's venue.
        /// </summary>
        public string Venue { get; set; }

        /// <summary>
        /// Gets or sets the ID of the league this session belongs to.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Whether or not this session has been finalized.
        /// </summary>
        public bool Finalized { get; set; }

        /// <summary>
        /// Gets or sets the user-session relations this session belongs to; autofilled when
        /// fetched from the database, null otherwise.
        /// </summary>
        public virtual List<UserSessionEntity> UserSessions { get; set; } = null;

        /// <summary>
        /// Gets or sets the league this session belongs to; autofilled when fetched from the
        /// database, null otherwise.
        /// </summary>
        public virtual LeagueEntity League { get; set; } = null;
    }
}
