namespace Application.Models.Result
{
    /// <summary>
    /// Successful result of an attempt to create a new league.
    /// </summary>
    public class CreateLeagueResultModel
    {
        /// <summary>
        /// The unique identifier of the created league.
        /// </summary>
        public int Id { get; set; }
    }
}
