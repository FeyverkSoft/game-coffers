using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Queries.Gamers;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Gamers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class GamersController : ControllerBase
    {
        private readonly IGamerRepository _gamerRepository;
        private readonly IQueryProcessor _queryProcessor;
        private readonly LoanFactory _loanFactory;

        public GamersController(IGamerRepository gamerRepository, IQueryProcessor queryProcessor)
        {
            _gamerRepository = gamerRepository;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This method Returns basic user information.
        /// </summary>
        [HttpGet("current")]
        [ProducesResponseType(typeof(BaseGamerInfoView), 200)]
        public async Task<ActionResult<BaseGamerInfoView>> GetMyInfo(CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserId();
            return Ok(await _queryProcessor.Process<GetBaseGamerInfoQuery, BaseGamerInfoView>(
                new GetBaseGamerInfoQuery
                {
                    UserId = userId
                }, cancellationToken));
        }

        /// <summary>
        /// This method add new character for gamer.
        /// </summary>
        [HttpPut("{gamerId}/characters")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCharacter(Guid gamerId, AddCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (!HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            await _gamerRepository.Load(gamer, cancellationToken);

            gamer.AddCharacters(binding.Name, binding.ClassName);

            await _gamerRepository.Save(gamer);

            return Ok();
        }

        /// <summary>
        /// This method delete character for gamer.
        /// </summary>
        [HttpDelete("{gamerId}/characters")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCharacter(Guid gamerId, DeleteCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (!HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            await _gamerRepository.Load(gamer, cancellationToken);

            gamer.DeleteCharacter(binding.Name);

            await _gamerRepository.Save(gamer);

            return Ok();
        }

        /// <summary>
        /// This method changed gamer status
        /// </summary>
        [HttpPatch("{gamerId}/status")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetStatus(Guid gamerId, PatchGamerStatusBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (!HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            gamer.SetStatus(binding.Status);

            await _gamerRepository.Save(gamer);

            return Ok();
        }

        /// <summary>
        /// This method changed gamer rank
        /// </summary>
        [HttpPatch("{gamerId}/rank")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetRole(Guid gamerId, PatchGamerRankBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (!HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            gamer.SetRank(binding.Rank);

            await _gamerRepository.Save(gamer);

            return Ok();
        }

        /// <summary>
        /// Добавить игроку новый займ
        /// </summary>
        [HttpPut("{gamerId}/loan")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PutLoan(Guid gamerId, PutLoanBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            var loan = _loanFactory.Build(binding.Id, gamer.GuildId, binding.Amount, binding.Description, binding.BorrowDate, binding.ExpiredDate);

            gamer.AddLoan(loan);

            await _gamerRepository.Save(gamer);

            return Ok();
        }

        /// <summary>
        /// ДОбавить игроку новый штраф
        /// </summary>
        [HttpPut("{gamerId}/penalty")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PutPenalty(Guid gamerId, PutPenaltyBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            gamer.AddPenalty(binding.Id, binding.Amount, binding.Description);

            await _gamerRepository.Save(gamer);

            return Ok();
        }
    }
}