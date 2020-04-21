using System;

using Application.Models.Output;

using Persistence.Entities;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a league.
    /// </summary>
    public class LeagueOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="LeagueOutputModel"/> to this output
        /// type.
        /// </summary>
        public static readonly Func<LeagueOutputModel, LeagueOutputType> FromModel
            = model => new LeagueOutputType { Model = model };

        /// <summary>
        /// The league's unique identifier.
        /// </summary>
        public int Id => this.Model.Id;

        /// <summary>
        /// The league's name.
        /// </summary>
        public string Name => this.Model.Name;

        /// <summary>
        /// The starting amount for users in this tournament.
        /// </summary>
        public int StartingAmount => this.Model.StartingAmount;

        /// <summary>
        /// Whether or not entities linked to this league can be changed.
        /// </summary>
        public bool AllowChanges => this.Model.AllowChanges;

        /// <summary>
        /// The type of the league.
        /// </summary>
        public LeagueType Type => this.Model.Type;

        internal LeagueOutputModel Model { get; set; }
    }
}
