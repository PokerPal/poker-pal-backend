namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Credentials provided by the user for an attempt to log in.
    /// </summary>
    public class LogInInputType
    {
        /// <summary>
        /// The email provided by the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password provided by the user.
        /// </summary>
        public string Password { get; set; }
    }
}
