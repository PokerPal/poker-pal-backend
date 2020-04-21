using System;

using Persistence.Entities;

namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to make a new league.
    /// </summary>
    public class CreateLeagueInputType
    {
        /// <summary>
        /// The league's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The starting amount for users in this league.
        /// </summary>
        public int StartingAmount { get; set; }

        /// <summary>
        /// If entities linked to this league can be changed.
        /// </summary>
        public bool AllowChanges { get; set; }

        /// <summary>
        /// The league's type.
        /// </summary>
        public LeagueType Type { get; set; }
    }
}
