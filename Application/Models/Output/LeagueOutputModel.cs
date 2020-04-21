using Persistence.Entities;

namespace Application.Models.Output
{
    /// <summary>
    /// Represents details about a league.
    /// </summary>
    public class LeagueOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueOutputModel"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the league.</param>
        /// <param name="name">The name of the league.</param>
        /// <param name="startingAmount">The starting amount for users in the tournament.</param>
        /// <param name="allowChanges">If the session linked to this leagues, entities can be changed.</param>
        /// <param name="type">The type of this league. If it is a cash or points game.</param>
        public LeagueOutputModel(int id, string name, int startingAmount, bool allowChanges, LeagueType type)
        {
            this.Id = id;
            this.Name = name;
            this.StartingAmount = startingAmount;
            this.AllowChanges = allowChanges;
            this.Type = type;
        }

        /// <summary>
        /// The unique identifier of the league.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The league's starting amount, the amount a user starts with.
        /// </summary>
        public int StartingAmount { get; set; }

        /// <summary>
        /// Whether or not events in this league can be changed.
        /// </summary>
        public bool AllowChanges { get; set; }

        /// <summary>
        /// The name of the league.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the league.
        /// </summary>
        public LeagueType Type { get; }
    }
}
