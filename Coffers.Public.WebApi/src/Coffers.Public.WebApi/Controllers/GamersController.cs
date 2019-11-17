using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Domain.Operations;
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
        private readonly IGuildRepository _guildRepository;
        private readonly LoanFactory _loanFactory;
        private readonly OperationService _operationService;

        public GamersController(
            IGamerRepository gamerRepository,
            IGuildRepository guildRepository,
            LoanFactory loanFactory,
            OperationService operationService)
        {
            _gamerRepository = gamerRepository;
            _guildRepository = guildRepository;
            _loanFactory = loanFactory;
            _operationService = operationService;
        }

        /// <summary>
        /// This method add new character for gamer.
        /// this method is available only to the officer or leader
        /// </summary>
        [HttpPut("{gamerId}/characters")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCharacter([FromRoute]Guid gamerId, [FromBody] AddCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            await _gamerRepository.Load(gamer, cancellationToken);

            gamer.AddCharacters(binding.Name, binding.ClassName, binding.IsMain);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// This method delete character for gamer.
        /// this method is available only to the officer or leader
        /// </summary>
        [HttpDelete("{gamerId}/characters")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCharacter([FromRoute]Guid gamerId, [FromBody] DeleteCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            await _gamerRepository.Load(gamer, cancellationToken);

            gamer.DeleteCharacter(binding.Name);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// This method update character info for gamer.
        /// this method is available only to the officer or leader
        /// </summary>
        [HttpPut("{gamerId}/characters/{name}")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EditCharacter(
            [FromRoute]Guid gamerId,
            [FromRoute]String name,
            [FromBody] EditCharacterBinding binding,
            CancellationToken cancellationToken)
        {
            var character = await _characterRepository.Get(HttpContext.GuildId(), gamerId, name, cancellationToken);

            if (character == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Character {name} not found");

            await _characterRepository.Save(character);

            return Ok(new { });
        }


        /// <summary>
        /// This method changed gamer status
        /// </summary>
        [HttpPatch("{gamerId}/status")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetStatus([FromRoute]Guid gamerId, [FromBody] PatchGamerStatusBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            gamer.SetStatus(binding.Status);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// This method changed gamer rank
        /// </summary>
        [HttpPatch("{gamerId}/rank")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetRole([FromRoute]Guid gamerId, [FromBody]PatchGamerRankBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, $"Gamer {gamerId} not found");

            gamer.SetRank(binding.Rank);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// Добавить игроку новый займ
        /// </summary>
        [HttpPut("{gamerId}/loans")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PutLoan([FromRoute]Guid gamerId, [FromBody]PutLoanBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer.Loans?.Any(_ => _.Id == binding.Id) == true)
                return Ok(new { });

            var guild = await _guildRepository.Get(gamer.GuildId, cancellationToken);

            var loan = await _loanFactory.Build(binding.Id, gamer.GuildId, gamer.Rank, binding.Amount,
                binding.Description, binding.BorrowDate, binding.ExpiredDate);

            gamer.AddLoan(loan);

            await _gamerRepository.Save(gamer).ContinueWith(async t =>
            {
                t.Wait(cancellationToken);
                await _operationService.PutLoan(loan.Id, loan.Account.Id, guild.GuildAccount.Id, loan.Amount, loan.TaxAmount);
            }, cancellationToken);

            return Ok(new { });
        }

        /// <summary>
        /// Добавить игроку новый штраф
        /// </summary>
        [HttpPut("{gamerId}/penalties")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PutPenalty([FromRoute]Guid gamerId, [FromBody] PutPenaltyBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");
            if (gamer.Penalties?.Any(_ => _.Id == binding.Id) == true)
                return Ok(new { });

            gamer.AddPenalty(binding.Id, binding.Amount, binding.Description);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// Отменить ещё не оплаченный  штраф
        /// </summary>
        [HttpDelete("{gamerId}/penalties/{penaltyId}")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelPenalty([FromRoute]Guid gamerId, [FromRoute] Guid penaltyId, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            if (gamer.Penalties?.Any(_ => _.Id == penaltyId) == false)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.PenaltyNotFound, "");

            gamer.CancelPenalty(penaltyId);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// Отменить ещё не оплаченный  займ
        /// </summary>
        [HttpDelete("{gamerId}/loans/{loanId}")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelLoan([FromRoute]Guid gamerId, [FromRoute]Guid loanId, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            gamer.CancelLoan(loanId);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// Реверс займа. Красное сторно.
        /// </summary>
        [HttpPost("{gamerId}/loans/{loanId}/reverse")]
        [PermissionRequired("admin", "officer", "leader")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ReverseLoan([FromRoute]Guid gamerId, [FromRoute]Guid loanId, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(gamerId, cancellationToken);

            if (gamer == null || !HttpContext.IsAdmin() && gamer.GuildId != HttpContext.GuildId())
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");

            gamer.CancelLoan(loanId);

            await _gamerRepository.Save(gamer)
                .ContinueWith(async c =>
                {
                    c.Wait(cancellationToken);
                    await _operationService.CancelLoan(loanId);
                }, cancellationToken);

            return Ok(new { });
        }
    }
}