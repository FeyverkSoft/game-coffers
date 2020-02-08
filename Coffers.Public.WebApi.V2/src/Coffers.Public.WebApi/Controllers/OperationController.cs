using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Operations;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Operation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class OperationController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader", "veteran")]
        [HttpPost("/operations")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewOperation(
            [FromServices] IOperationsRepository operationsRepository,
            [FromServices] OperationCreator operationCreator,
            [FromBody] AddOperationBinding binding,
            CancellationToken cancellationToken)
        {
            var operation = await operationsRepository.Get(binding.Id, cancellationToken);

            if (operation != null)
            {
                if (operation.Amount != binding.Amount ||
                    operation.UserId != binding.UserId ||
                    operation.DocumentId != binding.DocumentId ||
                    operation.GuildId != HttpContext.GetGuildId() ||
                    operation.Type != binding.Type)
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.OperationAlreadyExists, $"Operation {binding.Id} already exists",
                        new { operation.Id, operation.Amount, operation.Description, operation.Type, operation.UserId }.ToDictionary());
            }

            operation = await operationCreator.Create(
                binding.Id,
                HttpContext.GetGuildId(),
                binding.UserId,
                binding.DocumentId,
                binding.Amount,
                binding.Type,
                binding.Description,
                binding.ParentOperationId,
                cancellationToken);
            await operationsRepository.Save(operation);
            return Ok(operation);
        }
    }
}