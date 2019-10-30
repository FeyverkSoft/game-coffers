using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Queries.Profiles;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class ProfileController : ControllerBase
    {
        private readonly IGamerRepository _gamerRepository;
        private readonly IQueryProcessor _queryProcessor;

        public ProfileController(
            IGamerRepository gamerRepository,
            IQueryProcessor queryProcessor)
        {
            _gamerRepository = gamerRepository;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This method Returns basic user information.
        /// </summary>
        [HttpGet]
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
        /// This method add new character for current gamer.
        /// </summary>
        [HttpPut("characters")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCharacter([FromBody] AddCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(HttpContext.GetUserId(), cancellationToken);

            await _gamerRepository.Load(gamer, cancellationToken);

            gamer.AddCharacters(binding.Name, binding.ClassName);

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }

        /// <summary>
        /// This method delete character for current gamer.
        /// </summary>
        [HttpDelete("characters")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCharacter([FromBody] DeleteCharacterBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _gamerRepository.Get(HttpContext.GetUserId(), cancellationToken);

            await _gamerRepository.Load(gamer, cancellationToken);

            try
            {
                gamer.DeleteCharacter(binding.Name);
            }
            catch (CharacterNotFoundException e)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.CharacterNotFound, $"Character {e.Message} not found");
            }

            await _gamerRepository.Save(gamer);

            return Ok(new { });
        }
    }
}