namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to link a session to a user.
    /// </summary>
    public class CreateUserSessionInput
    {
        /// <summary>
        /// The ID of the user to link.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The users score in this session.
        /// </summary>
        public int TotalScore { get; set; }
    }
}