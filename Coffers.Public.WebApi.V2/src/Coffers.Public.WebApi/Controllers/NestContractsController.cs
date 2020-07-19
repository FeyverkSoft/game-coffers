using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffers.Public.Domain.NestContracts;
using Coffers.Public.Queries.NestContract;
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
        [ProducesResponseType(typeof(ICollection<NestContractView>), 200)]
        public async Task<ActionResult<ICollection<NestContractView>>> GetCurrentContracts(
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
        [ProducesResponseType(typeof(NestContractView), 200)]
        public async Task<ActionResult<NestContractView>> AddCurrentContracts(
            [FromBody] NestContractBinding binding,
            [FromServices] NestContractCreator contractCreator,
            [FromServices] INestContractRepository repository,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            try{
                var contract = await contractCreator.Create(HttpContext.GetUserId(), HttpContext.GetGuildId(), binding.Id, binding.NestId,
                    binding.Reward, binding.CharacterName, cancellationToken);
                await repository.Save(contract, cancellationToken);
            }
            catch (ContractAlreadyExistsException e){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.ContractAlreadyExists, e.Message);
            }
            catch (NestNotFoundException e){
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.NestNotFound, e.Message);
            }

            return Ok(await queryProcessor.Process<NestContractQuery, NestContractView>(new NestContractQuery(
                    guildId: HttpContext.GetGuildId(),
                    nestContractId: binding.Id),
                cancellationToken));
        }

        /// <summary>
        /// This method close contract
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/gamers/current/contracts/{contractId}")]
        [ProducesResponseType(typeof(NestContractView), 200)]
        public async Task<ActionResult<NestContractView>> CloseContract(
            [FromRoute] Guid contractId,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// This method get contract
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/gamers/contracts/{contractId}")]
        [ProducesResponseType(typeof(NestContractView), 200)]
        public async Task<ActionResult<NestContractView>> GetContract(
            [FromRoute] Guid contractId,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            return Ok(await queryProcessor.Process<NestContractQuery, NestContractView>(new NestContractQuery(
                    guildId: HttpContext.GetGuildId(),
                    nestContractId: contractId),
                cancellationToken));
        }
    }
}