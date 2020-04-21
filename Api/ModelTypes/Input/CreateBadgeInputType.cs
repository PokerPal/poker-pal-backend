namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to make a new badge.
    /// </summary>
    public class CreateBadgeInputType
    {
        /// <summary>
        /// The badge's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The badge's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The type of the badge. Possible values: <c>"A"</c>, <c>"B"</c>.
        /// </summary>
        public string BadgeType { get; set; }
    }
}
