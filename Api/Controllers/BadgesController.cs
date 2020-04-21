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
    /// Provides an interface to performing operations on and retrieving information about badges.
    /// </summary>
    [ApiController]
    [Route("badges")]
    public class BadgesController : ControllerBase
    {
        /// <summary>
        /// Create a new badge with the provided details.
        /// </summary>
        /// <param name="badge">Details of the badge to create.</param>
        /// <param name="badgeService">The badge service.</param>
        /// <returns>The result of the creation of the badge.</returns>
        [HttpPost("")]
        public async Task<ActionResult<Result<CreateBadgeResultType, string>>> CreateBadge(
            [FromBody] CreateBadgeInputType badge,
            [FromServices] BadgeService badgeService)
        {
            return (await badgeService.CreateBadge(badge.Name, badge.Description, badge.BadgeType))
                .Map(CreateBadgeResultType.FromModel);
        }

        /// <summary>
        /// Get the details of the badge with provided id.
        /// </summary>
        /// <param name="id">The unique identifier of the badge.</param>
        /// <param name="badgeService">The badge service.</param>
        /// <returns>The details of the badge.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<BadgeOutputType, string>>> GetBadge(
            [FromRoute] int id,
            [FromServices] BadgeService badgeService)
        {
            return (await badgeService.GetBadgeAsync(id))
                .Map(BadgeOutputType.FromModel)
                .WrapSplit<ActionResult>(this.Ok, this.NotFound);
        }
    }
}
