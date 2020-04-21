using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Api.ModelTypes.Input;
using Api.ModelTypes.Output;
using Api.ModelTypes.Result;

using Application.Services;

using Microsoft.AspNetCore.Mvc;

using Utility.ResultModel;

namespace Api.Controllers
{
    /// <summary>
    /// Provides an interface to performing operations on and retrieving information about leagues.
    /// </summary>
    [ApiController]
    [Route("leagues")]
    public class LeaguesController : ControllerBase
    {
        /// <summary>
        /// Create a new league with the provided details.
        /// </summary>
        /// <param name="league">Details of the league to create.</param>
        /// <param name="leagueService">The league service.</param>
        /// <returns>The result of the creation of the league.</returns>
        [HttpPost]
        public async Task<ActionResult<Result<CreateLeagueResultType, string>>> CreateLeague(
            [FromBody] CreateLeagueInputType league,
            [FromServices] LeagueService leagueService)
        {
            return (await leagueService.CreateLeague(
                    league.Name, league.StartingAmount, league.AllowChanges, league.Type))
                .Map(CreateLeagueResultType.FromModel);
        }

        /// <summary>
        /// Get the details of the league with provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the league.</param>
        /// <param name="leagueService">The league service.</param>
        /// <returns>The details of the league.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<LeagueOutputType, string>>> GetLeague(
            [FromRoute] int id,
            [FromServices] LeagueService leagueService)
        {
            return (await leagueService.GetLeagueAsync(id))
                .Map(LeagueOutputType.FromModel)
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the details of a user's standing within a league.
        /// </summary>
        /// <param name="leagueId">The unique identifier of the league.</param>
        /// <param name="userId">The information of the user.</param>
        /// <param name="context">
        /// the number of users both above and below (in terms of their place in the standings)
        /// the user that should be returned.
        /// </param>
        /// <param name="leagueService">The league service.</param>
        /// <returns>The details of the user leagues within the league.</returns>
        [HttpGet("{leagueId}/user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserLeagueOutputType>, string>>> GetUserLeague(
            [FromRoute] int leagueId,
            [FromRoute] int userId,
            int? context,
            [FromServices] LeagueService leagueService)
        {
            var places = context ?? 0;

            return (await leagueService.GetUserLeague(leagueId, userId, places))
                .Map(models => models
                    .Select(model => UserLeagueOutputType.FromModel(model))
                    .ToList())
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the details of the users history within the league.
        /// </summary>
        /// <param name="leagueId">The unique identifier of the league.</param>
        /// <param name="userId">The information of the user.</param>
        /// <param name="leagueService">The league service.</param>
        /// <returns>The details of the users history within the league.</returns>
        [HttpGet("{leagueId}/user/{userId}/history")]
        public async Task<ActionResult<Result<UserLeagueHistoryOutputType, string>>> GetUserLeagueHistory(
            [FromRoute] int leagueId,
            [FromRoute] int userId,
            [FromServices] LeagueService leagueService)
        {
            return (await leagueService.GetUserLeagueHistory(leagueId, userId))
                .Map(models => models
                    .Select(model => UserLeagueHistoryOutputType.FromModel(model))
                    .ToList())
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the details of a user league within this league.
        /// </summary>
        /// <param name="startPlace">
        /// The lowest-numbered place value to include in the results. Defaults to 1.
        /// </param>
        /// <param name="endPlace">
        /// The highest-numbered place value to include in the results. Defaults to last place.
        /// </param>
        /// <param name="id">The unique identifier of the league.</param>
        /// <param name="leagueService">The league service.</param>
        /// <returns>The details of the user leagues within the league.</returns>
        [HttpGet("{id}/users")]
        public async Task<ActionResult<Result<IEnumerable<UserLeagueOutputType>, string>>>
            GetUsersLeagues(
                int? startPlace,
                int? endPlace,
                [FromRoute] int id,
                [FromServices] LeagueService leagueService)
        {
            return (await leagueService.GetUserLeagues(id))
                .Map(models => models
                    .Select(model => UserLeagueOutputType.FromModel(model))
                    .ToList())
                .AndThen<IEnumerable<UserLeagueOutputType>>(userLeagues =>
                    {
                        // The provided indices are all 1-indexed, because they refer to "place
                        // value" rather than an index - so convert them to zero-based and also
                        // calculate defaults if they weren't provided.
                        var start = (startPlace ?? 1) - 1;
                        var end = Math.Min(endPlace ?? userLeagues.Count, userLeagues.Count);

                        if (start >= end)
                        {
                            return "Cannot get a list of user leagues with start place " +
                                   $"{start} > end place {end - 1}";
                        }

                        userLeagues = userLeagues.GetRange(start, end - start);

                        return Result.Ok(userLeagues.AsEnumerable());
                    });
        }
    }
}
