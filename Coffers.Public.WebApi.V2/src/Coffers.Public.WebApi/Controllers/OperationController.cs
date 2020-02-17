using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Queries.Operations;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Operation;
using Coffers.Types.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

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
                return Ok(operation);
            }

            try
            {
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
            }
            catch (DocumentNotFoundException e)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.DocumentNotFound, e.Message);
            }

            await operationsRepository.Save(operation);
            return Ok(operation);
        }

        [Authorize]
        [PermissionRequired("admin", "officer", "leader", "veteran")]
        [HttpPut("/operations/{id}/document")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AttachDocument(
            [FromRoute] Guid id,
            [FromServices] IOperationsRepository operationsRepository,
            [FromServices] DocumentSetter setter,
            [FromBody] AddOperationDocumentBinding binding,
            CancellationToken cancellationToken)
        {
            var operation = await operationsRepository.Get(id, cancellationToken);

            if (operation == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.OperationNotFound, $"Operation {id} not found");

            try
            {
                switch (binding.Type)
                {
                    case OperationType.Tax:
                        await setter.SetTax(operation, binding.DocumentId, cancellationToken);
                        break;
                    case OperationType.Penalty:
                        await setter.SetPenalty(operation, binding.DocumentId, cancellationToken);
                        break;
                    case OperationType.Loan:
                        await setter.SetLoan(operation, binding.DocumentId, cancellationToken);
                        break;
                    default:
                        throw new ApiException(HttpStatusCode.BadRequest, "Unsupported operation", "Unsupported operation");
                }
            }
            catch (DocumentNotFoundException)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.DocumentNotFound, $"Document {binding.DocumentId} of {binding.Type} not found");
            }

            await operationsRepository.Save(operation);

            return Ok(operation);
        }

        /// <summary>
        /// This method return gamer operation list from per
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/Guild/operations")]
        [ProducesResponseType(typeof(ICollection<OperationListView>), 200)]
        public async Task<ActionResult<OperationListView>> GetOperations(
            [FromQuery] GetOperationsBinding binding,
            [FromServices] QueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            return Ok(await queryProcessor.Process<GetOperationsQuery, ICollection<OperationListView>>(
                new GetOperationsQuery(
                    HttpContext.GetGuildId(),
                    binding.DateMonth?.Trunc(DateTruncType.Day)),
                cancellationToken));
        }
    }
}