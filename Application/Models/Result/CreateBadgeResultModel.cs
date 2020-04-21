namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new badge.
    /// </summary>
    public class CreateBadgeResultModel
    {
        /// <summary>
        /// The unique identifier of the created badge.
        /// </summary>
        public int Id { get; set; }
    }
}
