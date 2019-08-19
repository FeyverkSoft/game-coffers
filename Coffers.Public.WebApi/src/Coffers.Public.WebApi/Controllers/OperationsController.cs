using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Queries.Gamers;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.Queries.Operations;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Operations;
using Coffers.Types.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class OperationsController : ControllerBase
    {
        private readonly OperationService _operationService;
        private readonly IOperationsRepository _operationsRepository;
        private readonly IQueryProcessor _queryProcessor;

        public OperationsController(OperationService operationService, IQueryProcessor queryProcessor,
            IOperationsRepository operationsRepository)
        {
            _operationService = operationService;
            _queryProcessor = queryProcessor;
            _operationsRepository = operationsRepository;
        }

        /// <summary>
        /// This method Returns all operations by document id.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> GetOperations([FromQuery]OperationBinding binding, CancellationToken cancellationToken)
        {
            return Ok(await _queryProcessor.Process<GetOperationsQuery, ICollection<OperationView>>(
                new GetOperationsQuery
                {
                    DocumentId = binding.DocumentId,
                    Type = binding.Type
                }, cancellationToken));
        }

        /// <summary>
        /// This method Returns all operations by guild.
        /// </summary>
        [HttpGet("guild/{guildId}")]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> GetGuildOperations(
            [FromRoute] Guid guildId,
            [FromQuery] DateTime? dateMonth,
            CancellationToken cancellationToken)
        {
            var date = dateMonth ?? DateTime.UtcNow.Trunc(DateTruncType.Month);
            var guildAcc = await _queryProcessor.Process<GuildAccountQuery, GuildAccountView>(new GuildAccountQuery
            {
                GuildId = guildId
            }, cancellationToken);

            return Ok(await _queryProcessor.Process<GetOperationsByAccQuery, ICollection<OperationView>>(
                new GetOperationsByAccQuery
                {
                    AccountId = guildAcc.AccountId,
                    DateMonth = date.Trunc(DateTruncType.Month)
                }, cancellationToken));
        }

        /// <summary>
        /// This method Returns all operations by document id.
        /// </summary>
        [HttpGet("gamer/{userId}")]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> GetOperationsByUserId(
            [FromRoute] Guid userId,
            [FromQuery] DateTime? dateMonth,
            CancellationToken cancellationToken)
        {
            var date = dateMonth ?? DateTime.UtcNow.Trunc(DateTruncType.Month);
            var user = await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = userId
            }, cancellationToken);

            return Ok(await _queryProcessor.Process<GetOperationsByAccQuery, ICollection<OperationView>>(
                new GetOperationsByAccQuery
                {
                    AccountId = user.AccountId,
                    DateMonth = date.Trunc(DateTruncType.Month)
                }, cancellationToken));
        }

        /// <summary>
        /// This method add new operation
        /// </summary>
        [HttpPut]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> AddOperation(
            [FromBody] CreateOperationBinding binding,
            CancellationToken cancellationToken)
        {
            var operation = await _operationsRepository.Get(binding.Id, cancellationToken);
            if (operation != null && (operation.Type != binding.Type || operation.Amount != binding.Amount))
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.OperationAlreadyExists, "");
            }

            var guildAcc = await _queryProcessor.Process<GuildAccountQuery, GuildAccountView>(
                new GuildAccountQuery
                {
                    GuildId = HttpContext.GuildId()
                }, cancellationToken);

            try
            {
                switch (binding.Type)
                {
                    case OperationType.Tax:
                        await _operationService.AddTaxOperation(
                             binding.Id,
                             await GetAccountId(binding.FromUserId.Value, cancellationToken),
                             guildAcc.AccountId,
                             binding.Amount,
                             binding.Description);
                        break;
                    case OperationType.Penalty:
                        await _operationService.AddPenaltyOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            binding.PenaltyId.Value,
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Loan:
                        await _operationService.AddLoanOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            binding.LoanId.Value,
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Exchange:
                        if (binding.Amount < 0)
                            throw new ApiException(HttpStatusCode.BadRequest, ErrorCodes.IncorrectOperation, "");
                        await _operationService.DoExchangeOperation(
                            binding.Id,
                            await GetAccountId(binding.FromUserId.Value, cancellationToken),
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Output:
                        await _operationService.DoOutputOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            await GetAccountId(binding.ToUserId.Value, cancellationToken),
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.InternalOutput:
                        await _operationService.DoInternalOutputOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            await GetAccountId(binding.ToUserId.Value, cancellationToken),
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Emission:
                        await _operationService.EmissionOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.InternalEmission:
                        await _operationService.DoInternalEmissionOperation(
                            binding.Id,
                            await GetAccountId(binding.FromUserId.Value, cancellationToken),
                            guildAcc.AccountId,
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Sell:
                        await _operationService.AddSellOperation(
                            binding.Id,
                            guildAcc.AccountId,
                            binding.Amount,
                            binding.Description);
                        break;
                    case OperationType.Other:
                        if (binding.ToUserId != null && binding.FromUserId == null)
                        {
                            await _operationService.AddOtherOperation(
                                binding.Id,
                                await GetAccountId(binding.ToUserId.Value, cancellationToken),
                                binding.Amount,
                                binding.Description);
                        }
                        else if (binding.ToUserId != null && binding.FromUserId != null)
                        {
                            await _operationService.AddOtherOperation(
                                binding.Id,
                                await GetAccountId(binding.FromUserId.Value, cancellationToken),
                                await GetAccountId(binding.ToUserId.Value, cancellationToken),
                                binding.Amount,
                                binding.Description);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (OperationException ex)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.OperationAlreadyExists, ex.Message);
            }

            return Ok(new { });
        }

        private async Task<Guid> GetAccountId(Guid gamerId, CancellationToken cancellationToken)
        {
            return (await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = gamerId
            }, cancellationToken)).AccountId;
        }
    }
}