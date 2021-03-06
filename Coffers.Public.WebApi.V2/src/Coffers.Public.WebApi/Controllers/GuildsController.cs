﻿using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Roles;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Guild;
using Coffers.Types.Gamer;
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
        /// This method get current Guild info
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/Guilds/current")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildQuery, GuildView>(
                new GuildQuery(HttpContext.GetGuildId()), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }

        /// <summary>
        /// This method get current Guild balance info
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/Guilds/current/balance")]
        [ProducesResponseType(typeof(GuildBalanceView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBalance(CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildBalanceQuery, GuildBalanceView>(
                new GuildBalanceQuery(HttpContext.GetGuildId()), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }


        /// <summary>
        /// This method update user roles
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="rank">Ранг игрока</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader")]
        [HttpPatch("/Guilds/current/roles/{rank}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetOrUpdateGuildTax(
            [FromBody] UpdateUserRoleBinding binding,
            [FromRoute] GamerRank rank,
            [FromServices] IGuildRepository guildRepository,
            CancellationToken cancellationToken)
        {
            var guild = await guildRepository.Get(HttpContext.GetGuildId(), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            guild.AddOrUpdateRole(rank, binding.Tariff.LoanTax, binding.Tariff.ExpiredLoanTax, binding.Tariff.Tax);

            guildRepository.Save(guild);

            return Ok(new { });
        }


        /// <summary>
        /// This method return user roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/Guilds/current/roles")]
        [ProducesResponseType(typeof(ICollection<GuildRoleView>), 200)]
        public async Task<IActionResult> GetGuildTax(CancellationToken cancellationToken)
        {
            var roles = await _queryProcessor.Process<GuildRoleListQuery, ICollection<GuildRoleView>>(
                new GuildRoleListQuery(HttpContext.GetGuildId()), cancellationToken);
            return Ok(roles);
        }
    }
}