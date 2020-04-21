using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Api.ModelTypes.Input;
using Api.ModelTypes.Output;
using Api.ModelTypes.Result;

using Application.Models.Result;
using Application.Services;

using Microsoft.AspNetCore.Mvc;

using Utility.ResultModel;

namespace Api.Controllers
{
    /// <summary>
    /// Provides an interface to performing operations on and retrieving information about
    /// sessions.
    /// </summary>
    [ApiController]
    [Route("sessions")]
    public class SessionsController : ControllerBase
    {
        /// <summary>
        /// Create a new session with the provided details.
        /// </summary>
        /// <param name="session">Details of the session to create.</param>
        /// <param name="sessionService">The session service.</param>
        /// <returns>The result of the creation of the session.</returns>
        [HttpPost("")]
        public async Task<ActionResult<Result<CreateSessionResultType, string>>>
            CreateSession(
                [FromBody] CreateSessionInputType session,
                [FromServices] SessionService sessionService)
        {
            return (await sessionService.CreateSession(
                    session.StartDate,
                    session.EndDate,
                    session.Frequency,
                    session.Venue,
                    session.LeagueId))
                .Map(CreateSessionResultType.FromModel);
        }

        /// <summary>
        /// Get the details of the session with provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the session.</param>
        /// <param name="sessionService">The session service.</param>
        /// <returns>The details of the session.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<SessionOutputType, string>>> GetSession(
            [FromRoute] int id,
            [FromServices] SessionService sessionService)
        {
            return (await sessionService.GetSessionAsync(id))
                .Map(SessionOutputType.FromModel)
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the user sessions associated with a session.
        /// </summary>
        /// <param name="id">The unique identifier of the session.</param>
        /// <param name="sessionService">The session service.</param>
        /// <returns>The user sessions associated with the session.</returns>
        [HttpGet("{id}/users")]
        public async Task<ActionResult<Result<IEnumerable<UserSessionOutputType>, string>>>
        GetSessionsUsers(
            [FromRoute] int id,
            [FromServices] SessionService sessionService)
        {
            return (await sessionService.GetUserSessions(id))
                .Map(models => models
                    .Select(model => UserSessionOutputType.FromModel(model))
                    .ToList())
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Finalize a session.
        /// </summary>
        /// <param name="id">the ID of the session to finalize.</param>
        /// <param name="sessionService">The session service.</param>
        /// <returns>The result of the creation of the user.</returns>
        [HttpPost("{id}/finalize")]
        public async Task<ActionResult<Result<FinalizeSessionResultType, string>>> FinalizeSession(
            [FromRoute] int id,
            [FromServices] SessionService sessionService)
        {
            return (await sessionService.FinalizeSession(id))
                .Map(FinalizeSessionResultType.FromModel);
        }

        /// <summary>
        /// Give a session a users information.
        /// </summary>
        /// <param name="id">The session id to be used.</param>
        /// <param name="userSessionInput">Details of the session and user.</param>
        /// <param name="sessionService">The session service.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("{id}/users")]
        public async Task<ActionResult<Result<CreateUserSessionResultType, string>>> AddUserSession(
            [FromRoute] int id,
            [FromBody] CreateUserSessionInput userSessionInput,
            [FromServices] SessionService sessionService)
        {
            return (await sessionService.AddUser(id, userSessionInput.UserId, userSessionInput.TotalScore))
                .Map(CreateUserSessionResultType.FromModel);
        }
    }
}
