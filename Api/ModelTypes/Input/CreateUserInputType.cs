namespace Api.ModelTypes.Input
{
    /// <summary>
    /// The details required to create a new user.
    /// </summary>
    public class CreateUserInputType
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's chosen password.
        /// </summary>
        public string Password { get; set; }
    }
}
