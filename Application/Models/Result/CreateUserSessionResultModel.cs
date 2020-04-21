namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new user session.
    /// </summary>
    public class CreateUserSessionResultModel
    {
        /// <summary>
        /// The unique identifier of the session.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// The unique identifier of the User.
        /// </summary>
        public int UserId { get; set; }
    }
}