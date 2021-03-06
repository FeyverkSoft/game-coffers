﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Admin.GuildCreate;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Guild;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    [PermissionRequired(new[] {"admin"})]
    public class GuildsController : ControllerBase
    {
        private readonly IGuildRepository _guildRepository;
        private readonly IQueryProcessor _queryProcessor;

        public GuildsController(
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
        [HttpPost("/admin/guilds")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create(
            [FromBody] GuildCreateBinding binding,
            [FromServices] GuildCreator creator,
            CancellationToken cancellationToken)
        {
            try
            {
                var guild = await creator.Create(id: binding.Id, name: binding.Name, status: binding.Status, recruitmentStatus: binding.RecruitmentStatus,
                    cancellationToken);

                await _guildRepository.Save(guild);

                return CreatedAtRoute("GetGuild", new {id = binding.Id}, null);
            }
            catch (GuildAlreadyExistsException e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GuildAlreadyExists, e.Message);
            }
        }

        /// <summary>
        /// This method get Guild info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(template: "/admin/guilds/{id}", Name = "GetGuild")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var guild = await _queryProcessor.Process<GuildQuery, GuildView>(new GuildQuery(id), cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }
    }
}