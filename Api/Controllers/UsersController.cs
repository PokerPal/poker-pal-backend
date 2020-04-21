using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

using Api.ModelTypes.Input;
using Api.ModelTypes.Output;
using Api.ModelTypes.Result;

using Application.Models.Output;
using Application.Models.Result;
using Application.Services;

using Microsoft.AspNetCore.Mvc;

using Utility.ResultModel;

namespace Api.Controllers
{
    /// <summary>
    /// Provides an interface to performing operations on and retrieving information about users.
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Create a new user with the provided details.
        /// </summary>
        /// <param name="user">Details of the user to create.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The result of the creation of the user.</returns>
        [HttpPost("")]
        public async Task<ActionResult<Result<CreateUserResultType, string>>> CreateUser(
            [FromBody] CreateUserInputType user,
            [FromServices] UserService userService)
        {
            return (await userService.CreateUserAsync(user.Email, user.Name, user.Password))
                .Map(CreateUserResultType.FromModel);
        }

        /// <summary>
        /// Attempt to log in with the provided details.
        /// </summary>
        /// <param name="logIn">The credentials provided by the user.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The result of the attempt to log in.</returns>
        [HttpPost("logIn")]
        public async Task<ActionResult<Result<LogInResultType, string>>> LogIn(
            [FromBody] LogInInputType logIn,
            [FromServices] UserService userService)
        {
            return (await userService.VerifyUserAsync(logIn.Email, logIn.Password))
                .Map(LogInResultType.FromModel);
        }

        /// <summary>
        /// Delete a user with the provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the user to be deleted.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The result of the deletion of the user.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<DeleteUserResultType, string>>> DeleteUser(
            [FromRoute] int id,
            [FromServices] UserService userService)
        {
            return (await userService.DeleteUserAsync(id))
                .Map(DeleteUserResultType.FromModel);
        }

        /// <summary>
        /// Get the details of the user with the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The details of the user.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserOutputType, string>>> GetUser(
            [FromRoute] int id,
            [FromServices] UserService userService)
        {
            return (await userService.GetUserAsync(id))
                .Map(UserOutputType.FromModel)
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the users win or loss streak from a given league.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="leagueId">The unique identifier of the league.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The details of the users streak.</returns>
        [HttpGet("{userId}/streak/{leagueId}")]
        public async Task<ActionResult<Result<UserStreakOutputType, string>>> GetUserStreak(
            [FromRoute] int userId,
            [FromRoute] int leagueId,
            [FromServices] UserService userService)
        {
            return (await userService.GetUserStreakAsync(userId, leagueId))
                .Map(UserStreakOutputType.FromModel)
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the details of all users.
        /// </summary>
        /// <param name="q">Search query to filter users.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The details of the user.</returns>
        [HttpGet("")]
        public async Task<ActionResult<Result<IEnumerable<UserOutputType>, string>>> GetUsers(
            string q,
            [FromServices] UserService userService)
        {
            if (string.IsNullOrEmpty(q))
            {
                return (await userService.GetAllUsersAsync())
                    .Map(users => users.Select(UserOutputType.FromModel))
                    .WrapSplit<ActionResult>(this.Ok, this.NotFound);
            }

            return (await userService.SearchUsersAsync(q))
                .Map(users => users.Select(UserOutputType.FromModel));
        }

        /// <summary>
        /// Get the sessions a user has participated in.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The sessions the user participated in.</returns>
        [HttpGet("{id}/sessions")]
        public async Task<ActionResult<Result<IEnumerable<SessionOutputType>, string>>>
            GetUserSessions(
                [FromRoute] int id,
                [FromServices] UserService userService)
        {
            return (await userService.GetUserSessions(id))
                .Map(models => models
                    .Select(SessionOutputType.FromModel)
                    .ToList())
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Get the badges a user has.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The badges the user has.</returns>
        [HttpGet("{id}/badges")]
        public async Task<ActionResult<Result<IEnumerable<BadgeOutputType>, string>>> GetUserBadges(
            [FromRoute] int id,
            [FromServices] UserService userService)
        {
            return (await userService.GetUserBadges(id))
                .Map(models => models
                    .Select(model => BadgeOutputType.FromModel(model))
                    .ToList())
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }

        /// <summary>
        /// Give a user the specified badge.
        /// </summary>
        /// <param name="id">The user id to be used.</param>
        /// <param name="inputType">Details of the badge to associate with the user.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("{id}/badges")]
        public async Task<ActionResult<Result<CreateUserBadgeResultType, string>>> AddUserBadge(
            [FromRoute] int id,
            [FromBody] AddUserBadgeInputType inputType,
            [FromServices] UserService userService)
        {
            return (await userService.AddBadge(id, inputType.BadgeId))
                .Map(CreateUserBadgeResultType.FromModel);
        }
    }
}
