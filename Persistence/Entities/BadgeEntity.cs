using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    /// <summary>
    /// The type of the badge, further details on what the options are are to be decided with the
    /// group.
    /// </summary>
    public enum BadgeType
    {
        /// <summary>
        /// Option A.
        /// </summary>
        OptionA = 1,

        /// <summary>
        /// Option B.
        /// </summary>
        OptionB,
    }

    /// <summary>
    /// Represents a Badge in the database.
    /// </summary>
    public class BadgeEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeEntity"/> class.
        /// </summary>
        /// <param name="id">The badge's unique identifier.</param>
        /// <param name="name">The badge's Name.</param>
        /// <param name="description">The badge's Description.</param>
        /// <param name="type">The badge's type.</param>
        public BadgeEntity(int id, string name, string description, BadgeType type)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets this badge's unique identifier.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this badge's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets this badge's Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of this badge.
        /// </summary>
        public BadgeType Type { get; set; }

        /// <summary>
        /// Gets or sets the user-badge relations this badge belongs to; autofilled when fetched
        /// from the database, null otherwise.
        /// </summary>
        public virtual List<UserBadgeEntity> UserBadges { get; set; } = null;
    }
}
