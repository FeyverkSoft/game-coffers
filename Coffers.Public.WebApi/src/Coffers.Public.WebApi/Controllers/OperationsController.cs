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
        private readonly IQueryProcessor _queryProcessor;

        public OperationsController(OperationService operationService, IQueryProcessor queryProcessor)
        {
            _operationService = operationService;
            _queryProcessor = queryProcessor;
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
        /// This method Returns all operations by document id.
        /// </summary>
        [HttpGet("gamer/{userId}")]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> GetOperationsByUserId(
            [FromRoute] Guid userId,
            [FromQuery] DateTime? dateFrom,
            CancellationToken cancellationToken)
        {
            var date = dateFrom ?? DateTime.UtcNow.Trunc(DateTruncType.Month);
            var user = await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = userId
            }, cancellationToken);

            return Ok(await _queryProcessor.Process<GetOperationsByAccQuery, ICollection<OperationView>>(
                new GetOperationsByAccQuery
                {
                    AccountId = user.AccountId,
                    DateFrom = date.Trunc(DateTruncType.Day)
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
            var fromGamer = binding.FromUserId != null ? await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = binding.FromUserId.Value
            }, cancellationToken) : null;

            var toGamer = binding.ToUserId != null ? await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = binding.ToUserId.Value
            }, cancellationToken) : null;

            var guildAcc = await _queryProcessor.Process<GuildAccountQuery, GuildAccountView>(new GuildAccountQuery
            {
                GuildId = fromGamer?.GuildId ?? toGamer?.GuildId ?? HttpContext.GuildId()
            }, cancellationToken);

            switch (binding.Type)
            {
                case OperationType.Tax:
                    await _operationService.AddTaxOperation(
                         fromGamer.AccountId,
                         guildAcc.AccountId,
                         binding.Amount,
                         binding.Description);
                    break;
                case OperationType.Penalty:
                    await _operationService.AddPenaltyOperation(
                        fromGamer.AccountId,
                        guildAcc.AccountId,
                        binding.PenaltyId.Value,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Loan:
                    await _operationService.AddLoanOperation(
                        fromGamer.AccountId,
                        guildAcc.AccountId,
                        binding.LoanId.Value,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Exchange:
                    if (binding.Amount < 0)
                        throw new ApiException(HttpStatusCode.BadRequest, ErrorCodes.IncorrectOperation, "");
                    await _operationService.AddOtherOperation(
                        fromGamer.AccountId,
                        fromGamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Reward:
                    await _operationService.AddRewardOperation(
                        guildAcc.AccountId,
                        toGamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Salary:
                    await _operationService.AddSalaryOperation(
                        guildAcc.AccountId,
                        toGamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Sell:
                    await _operationService.AddOtherOperation(
                        guildAcc.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Other:
                    if (binding.ToUserId != null && binding.FromUserId == null)
                    {
                        await _operationService.AddOtherOperation(
                            toGamer.AccountId,
                            binding.Amount,
                            binding.Description);
                    }
                    else if (binding.ToUserId != null && binding.FromUserId != null)
                    {
                        await _operationService.AddOtherOperation(
                            fromGamer.AccountId,
                            toGamer.AccountId,
                            binding.Amount,
                            binding.Description);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Ok(new { });
        }
    }
}