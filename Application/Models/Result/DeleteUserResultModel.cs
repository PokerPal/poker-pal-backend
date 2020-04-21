namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to delete a user.
    /// </summary>
    public class DeleteUserResultModel
    {
        /// <summary>
        /// The unique identifier of the user deleted.
        /// </summary>
        public int Id { get; set; }
    }
}