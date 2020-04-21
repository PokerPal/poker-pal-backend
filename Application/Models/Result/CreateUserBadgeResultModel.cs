namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new userbadge.
    /// </summary>
    public class CreateUserBadgeResultModel
    {
        /// <summary>
        /// The unique identifier of the Badge.
        /// </summary>
        public int BadgeId { get; set; }

        /// <summary>
        /// The unique identifier of the User.
        /// </summary>
        public int UserId { get; set; }
    }
}
