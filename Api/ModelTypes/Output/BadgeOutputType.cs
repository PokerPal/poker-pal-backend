using System;

using Application.Models.Output;

namespace Api.ModelTypes.Output
{
    /// <summary>
    /// Details about a Badge.
    /// </summary>
    public class BadgeOutputType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="BadgeOutputModel"/> to this output type.
        /// </summary>
        public static readonly Func<BadgeOutputModel, BadgeOutputType> FromModel
            = model => new BadgeOutputType { Model = model };

        /// <summary>
        /// The badge's unique identifier.
        /// </summary>
        public string Id => this.Model.Id.ToString();

        /// <summary>
        /// The badge's name.
        /// </summary>
        public string Name => this.Model.Name.ToString();

        /// <summary>
        /// Description of the badge.
        /// </summary>
        public string Description => this.Model.Description.ToString();

        /// <summary>
        /// The badge type.
        /// </summary>
        public string BadgeType => this.Model.BadgeType.ToString();

        internal BadgeOutputModel Model { get; set; }
    }
}
