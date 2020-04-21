namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to link a badge to a user.
    /// </summary>
    public class AddUserBadgeInputType
    {
        /// <summary>
        /// The ID of the badge to link.
        /// </summary>
        public int BadgeId { get; set; }
    }
}
