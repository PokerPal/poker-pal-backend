namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new user.
    /// </summary>
    public class CreateUserResultModel
    {
        /// <summary>
        /// The unique identifier of the created user.
        /// </summary>
        public int Id { get; set; }
    }
}
