using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Queries.Gamers;
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
        private readonly IGuildRepository _guildRepository;
        private readonly IQueryProcessor _queryProcessor;

        public GuildsController(IGuildRepository guildRepository, IQueryProcessor queryProcessor)
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
        [PermissionRequired("admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create(GuildCreateBinding binding, CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

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
                recruitmentStatus: binding.RecruitmentStatus
                );

            await _guildRepository.Save(guild);

            return CreatedAtRoute("GetGuild", new { id = binding.Id }, null);
        }

        /// <summary>
        /// This method get Guilds list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GuildView>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {

            var guild = await _queryProcessor.Process<GuildsQuery, ICollection<GuildView>>(
                new GuildsQuery(), cancellationToken);

            return Ok(guild);
        }

        /// <summary>
        /// This method get Guild info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}", Name = "GetGuild")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin() && id != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            var guild = await _queryProcessor.Process<GuildQuery, GuildView>(
                new GuildQuery
                {
                    Id = id
                }, cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }

        /// <summary>
        /// This method get Guild balance info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}/balance")]
        [ProducesResponseType(typeof(GuildBalanceView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBalance([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin() && id != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            var guild = await _queryProcessor.Process<GuildBalanceQuery, GuildBalanceView>(
                new GuildBalanceQuery
                {
                    GuildId = id
                }, cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(guild);
        }

        /// <summary>
        /// This method register new Gamer in guild
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("{id}/Gamers")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewGamer([FromRoute]Guid id,
            [FromBody] GamerCreateBinding binding,
            CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin() && id != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            var guild = await _guildRepository.Get(id, cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            await _guildRepository.LoadGamers(guild, cancellationToken);

            if (guild.Gamers.Any(g =>
                g.Login.Equals(binding.Login, StringComparison.InvariantCultureIgnoreCase) &&
                !new[] { GamerStatus.Banned, GamerStatus.Left }.Contains(g.Status))
            )
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GamerAlreadyExists, "Gamer already exists");

            if (guild.Gamers.Any(g =>
                g.Login.Equals(binding.Login, StringComparison.InvariantCultureIgnoreCase) &&
                new[] { GamerStatus.Banned, GamerStatus.Left }.Contains(g.Status))
            )
                guild.AddGamer(binding.Login);
            else
                guild.AddGamer(binding.Id, binding.Name, binding.Login, binding.DateOfBirth, binding.Status, binding.Rank);

            await _guildRepository.Save(guild);

            return Ok();
        }

        /// <summary>
        /// This method update guild tariff
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPatch("{id}/Tariff")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetOrUpdateGuildTax([FromRoute]Guid id,
            [FromBody] UpdateTariffBinding binding,
            CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin() && id != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            var guild = await _guildRepository.Get(id, cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            if (binding.BeginnerTariff != null)
                guild.UpdateBeginnerTariff(binding.BeginnerTariff.LoanTax,
                    binding.BeginnerTariff.ExpiredLoanTax, binding.BeginnerTariff.Tax);
            if (binding.OfficerTariff != null)
                guild.UpdateOfficerTariff(binding.OfficerTariff.LoanTax,
                    binding.OfficerTariff.ExpiredLoanTax, binding.OfficerTariff.Tax);
            if (binding.LeaderTariff != null)
                guild.UpdateLeaderTariff(binding.LeaderTariff.LoanTax,
                    binding.LeaderTariff.ExpiredLoanTax, binding.LeaderTariff.Tax);
            if (binding.VeteranTariff != null)
                guild.UpdateVeteranTariff(binding.VeteranTariff.LoanTax,
                    binding.VeteranTariff.ExpiredLoanTax, binding.VeteranTariff.Tax);
            if (binding.SoldierTariff != null)
                guild.UpdateSoldierTariff(binding.SoldierTariff.LoanTax,
                    binding.SoldierTariff.ExpiredLoanTax, binding.SoldierTariff.Tax);

            await _guildRepository.Save(guild);

            return Ok();
        }

        /// <summary>
        /// This method return gamer list
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}/gamers")]
        [ProducesResponseType(200, Type = typeof(ICollection<GamersListView>))]
        public async Task<IActionResult> GetGamers([FromRoute]Guid id,
            [FromQuery] GetGamersBinding binding,
            CancellationToken cancellationToken)
        {
            if (!HttpContext.IsAdmin() && id != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            return Ok(await _queryProcessor.Process<GetGamersQuery, ICollection<GamersListView>>(
                new GetGamersQuery
                {
                    GuildId = id,
                    DateFrom = binding.DateFrom?.Trunc(DateTruncType.Day),
                    DateTo = binding.DateTo?.Trunc(DateTruncType.Day),
                    GamerStatuses = binding.GamerStatuses
                }, cancellationToken
                ));

        }
    }
}