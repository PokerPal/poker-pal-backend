namespace Api.ModelTypes.Input
{
    /// <summary>
    /// Details required to get a userLeague.
    /// </summary>
    public class GetUserLeagueInputType
    {
        /// <summary>
        /// The ID of the user to link.
        /// </summary>
        public int UserId { get; set; }
    }
}