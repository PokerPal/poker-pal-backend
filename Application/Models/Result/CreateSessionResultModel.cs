namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new session.
    /// </summary>
    public class CreateSessionResultModel
    {
        /// <summary>
        /// The unique identifier of the created session.
        /// </summary>
        public int Id { get; set; }
    }
}
