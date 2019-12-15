using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Guilds;
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
    [PermissionRequired("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IGuildRepository _guildRepository;
        private readonly IQueryProcessor _queryProcessor;

        public AdminController(
            IGuildRepository guildRepository,
            IQueryProcessor queryProcessor)
        {
            _guildRepository = guildRepository;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This method register new Guild in guild coffer service
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("guilds")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create(
            [FromBody]GuildCreateBinding binding,
            CancellationToken cancellationToken)
        {
            var existGuild = await _guildRepository.Get(binding.Id, cancellationToken);

            if (existGuild != null)
            {
                if (existGuild.Name == binding.Name)
                    return CreatedAtRoute("GetGuild", new { id = binding.Id }, null);

                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GuildAlreadyExists, "Гильдия уже существует");
            }

            var guild = new Guild(
                id: binding.Id,
                name: binding.Name,
                status: binding.Status,
                recruitmentStatus: binding.RecruitmentStatus);

            await _guildRepository.Save(guild);

            return CreatedAtRoute("AdminGetGuild", new { id = binding.Id }, null);
        }

        /// <summary>
        /// This method get Guild info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet(template: "guilds/{id}", Name = "GetGuild")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromRoute]Guid id,
            CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildQuery, GuildView>(new GuildQuery(id), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }
    }
}