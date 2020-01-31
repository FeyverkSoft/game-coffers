using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Penalties;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.User;
using Coffers.Types.Gamer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class PenaltyController : ControllerBase
    {
        /// <summary>
        /// This method add new penalty for user
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userId"></param>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method cancel Penalty
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "admin")]
        [HttpPost("/penalties/{id}/cancel")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelPenalty(
            [FromServices] IPenaltyRepository repository,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var penalty = await repository.Get(id, HttpContext.GetGuildId(), cancellationToken);

            if (penalty == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.PenaltyNotFound, $"Penalty {id} not found");

            try
            {
                penalty.MakeCancel();
                await repository.Save(penalty);
            }
            catch (InvalidOperationException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect penalty state");
            }

            return Ok(new { });
        }

        /// <summary>
        /// This method process Penalty
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "veteran", "admin")]
        [HttpPost("/penalties/{id}/process")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ProcessPenalty(
            [FromServices] IPenaltyRepository repository,
            [FromRoute] Guid id,
            [FromServices] PenaltyProcessor processor,
            CancellationToken cancellationToken)
        {
            var penalty = await repository.Get(id, HttpContext.GetGuildId(), cancellationToken);

            if (penalty == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.PenaltyNotFound, $"Penalty {id} not found");

            try
            {
                await processor.Process(penalty, cancellationToken);
                await repository.Save(penalty);
            }
            catch (InvalidOperationException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect penalty state");
            }

            return Ok(new { });
        }
    }
}