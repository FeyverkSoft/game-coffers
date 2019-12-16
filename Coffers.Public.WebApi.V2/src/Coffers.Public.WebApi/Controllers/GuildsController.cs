using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
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
        /// <param name="id"></param>
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

        /*
       /// <summary>
       /// This method register new Gamer in guild
       /// </summary>
       /// <param name="binding"></param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
       [Authorize]
       [PermissionRequired("admin", "officer", "leader")]
       [HttpPost("gamers")]
       [ProducesResponseType(200)]
       public async Task<IActionResult> AddNewGamer(
           [FromBody] GamerCreateBinding binding,
           CancellationToken cancellationToken)
       {
           var guild = await _guildRepository.Get(HttpContext.GuildId(), cancellationToken);

           if (guild == null)
               throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

           await _guildRepository.Save(guild);

           return Ok(new { });
       }
       */
        /*
        /// <summary>
        /// This method update guild tariff
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPatch("Tariff")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetOrUpdateGuildTax(
            [FromBody] UpdateTariffBinding binding,
            CancellationToken cancellationToken)
        {

            var guild = await _guildRepository.Get(HttpContext.GuildId(), cancellationToken);

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

            return Ok(new { });
        }

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