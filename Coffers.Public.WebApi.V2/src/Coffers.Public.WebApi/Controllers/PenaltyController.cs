using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Penalties;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class PenaltyController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("officer", "leader")]
        [HttpPost("/users/{userId}/penalties")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewPenalty(
            [FromServices] IUserRepository repository,
            [FromRoute] Guid userId,
            [FromBody] AddPenaltyBinding binding,
            CancellationToken cancellationToken)
        {
            var user = await repository.Get(userId, HttpContext.GetGuildId(), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"User {userId} not found");

            try
            {
                user.AddPenalty(binding.Id, binding.Amount, binding.Description);
                await repository.Save(user);
            }
            catch (PenaltyAlreadyExistsException e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.PenaltyAlreadyExists, $"Penalty {binding.Id} already exists", e.Detail.ToDictionary());
            }

            return Ok(new { });
        }
    }
}