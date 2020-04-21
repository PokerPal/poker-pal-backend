namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to finalize a session.
    /// </summary>
    public class FinalizeSessionResultModel
    {
        /// <summary>
        /// The unique identifier of the session being finalized.
        /// </summary>
        public int Id { get; set; }
    }
}