﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Domain.Roles;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Guild;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class GuildsController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public GuildsController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This method get Guild info
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("current")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildQuery, GuildView>(
                new GuildQuery(HttpContext.GuildId()), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }

        /// <summary>
        /// This method get Guild balance info
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("balance")]
        [ProducesResponseType(typeof(GuildBalanceView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBalance(CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildBalanceQuery, GuildBalanceView>(
                new GuildBalanceQuery(HttpContext.GuildId()), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }


        /// <summary>
        /// This method update user roles
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader")]
        [HttpPatch("roles")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetOrUpdateGuildTax(
            [FromBody] UpdateUserRoleBinding binding,
            [FromServices] IGuildRepository guildRepository,
            CancellationToken cancellationToken)
        {
            var guild = await guildRepository.Get(HttpContext.GuildId(), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            guild.AddOrUpdateRole(binding.Rank, binding.Tariff.LoanTax, binding.Tariff.ExpiredLoanTax, binding.Tariff.Tax);

            guildRepository.Save(guild);

            return Ok(new { });
        }

        /*
        /// <summary>
        /// This method return gamer list
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("gamers")]
        [ProducesResponseType(typeof(ICollection<GamersListView>), 200)]
        public async Task<ActionResult<GamersListView>> GetGamers(
            [FromQuery] GetGamersBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(await _queryProcessor.Process<GetGamersQuery, ICollection<GamersListView>>(
                new GetGamersQuery
                {
                    GuildId = HttpContext.GuildId(),
                    Month = binding.DateMonth?.Trunc(DateTruncType.Day),
                    GamerStatuses = binding.GamerStatuses
                }, cancellationToken
                ));

        }*/
    }
}