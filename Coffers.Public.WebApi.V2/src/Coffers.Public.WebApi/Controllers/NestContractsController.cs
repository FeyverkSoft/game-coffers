using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffers.Public.Domain.NestContracts;
using Coffers.Public.Queries.Users;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.NestContract;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class NestContracts : ControllerBase
    {
        /// <summary>
        /// This method return contract list
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/gamers/current/contracts")]
        [ProducesResponseType(typeof(ICollection<GamersListView>), 200)]
        public async Task<ActionResult<GamersListView>> GetCurrentContracts(
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// This method add new contract
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("/gamers/current/contracts")]
        [ProducesResponseType(typeof(ICollection<GamersListView>), 200)]
        public async Task<ActionResult<GamersListView>> AddCurrentContracts(
            [FromBody] NestContractBinding binding,
            [FromServices] NestContractCreator contractCreator,
            [FromServices] INestContractRepository repository,
            CancellationToken cancellationToken)
        {
            try{
                var contract = await contractCreator.Create(HttpContext.GetUserId(), HttpContext.GetGuildId(), binding.NestId, binding.Id,
                    binding.Reward, binding.CharacterName, cancellationToken);
                await repository.Save(contract, cancellationToken);
            }
            catch (ContractAlreadyExistsException e){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.ContractAlreadyExists, e.Message);
            }
            catch (NestNotFoundException e){
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.NestNotFound, e.Message);
            }

            return Ok(new { });
        }

        /// <summary>
        /// This method close contract
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/gamers/current/contracts/{contractId}")]
        [ProducesResponseType(typeof(ICollection<GamersListView>), 200)]
        public async Task<ActionResult<GamersListView>> CloseContract(
            [FromRoute] Guid contractId,
            CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}