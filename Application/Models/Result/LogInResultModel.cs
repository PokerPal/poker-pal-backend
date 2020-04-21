namespace Application.Models.Result
{
    /// <summary>
    /// The result of a successful attempt to log in.
    /// </summary>
    public class LogInResultModel
    {
        /// <summary>
        /// The user's ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's registered email address.
        /// </summary>
        public string Email { get; set; }
    }
}
