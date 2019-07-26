using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
        /// This method add new operation
        /// </summary>
        [HttpPut]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> AddOperation(
            [FromBody] CreateOperationBinding binding,
            CancellationToken cancellationToken)
        {
            var gamer = await _queryProcessor.Process<GetGamerInfoQuery, GamerInfoView>(new GetGamerInfoQuery
            {
                UserId = binding.FromUserId
            }, cancellationToken);
            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");
            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, "");

            var guildAcc = await _queryProcessor.Process<GuildAccountQuery, GuildAccountView>(new GuildAccountQuery
            {
                GuildId = gamer.GuildId
            }, cancellationToken);

            switch (binding.Type)
            {
                case OperationType.Tax:
                    await _operationService.AddTaxOperation(
                         gamer.AccountId,
                         guildAcc.AccountId,
                         binding.Amount,
                         binding.Description);
                    break;
                case OperationType.Penalty:
                    await _operationService.AddPenaltyOperation(
                        gamer.AccountId,
                        guildAcc.AccountId,
                        binding.PenaltyId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Loan:
                    await _operationService.AddLoanOperation(
                        gamer.AccountId,
                        guildAcc.AccountId,
                        binding.LoanId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Exchange:
                    if (binding.Amount < 0)
                        throw new ApiException(HttpStatusCode.BadRequest, ErrorCodes.IncorrectOperation, "");
                    await _operationService.AddOtherOperation(
                        gamer.AccountId,
                        gamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Reward:
                    await _operationService.AddRewardOperation(
                        guildAcc.AccountId,
                        gamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                case OperationType.Salary:
                    await _operationService.AddSalaryOperation(
                        guildAcc.AccountId,
                        gamer.AccountId,
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
                    await _operationService.AddOtherOperation(
                        gamer.AccountId,
                        binding.Amount,
                        binding.Description);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Ok(new { });
        }
    }
}