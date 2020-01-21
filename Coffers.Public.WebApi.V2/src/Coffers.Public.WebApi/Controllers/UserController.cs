using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.Domain.Users;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Guild;
using Coffers.Public.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffers.Helpers;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class UserController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/guild/gamers")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewGamer(
            [FromBody] GamerCreateBinding binding,
            [FromServices] Domain.UserRegistration.IUserRepository userRepository,
            [FromServices] UserFactory gamerFactory,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(binding.Id, cancellationToken);

            if (user != null)
                if (user.Login == binding.Login &&
                    user.Name == binding.Name &&
                    user.DateOfBirth == binding.DateOfBirth)
                    return Ok(new { });
                else
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GamerAlreadyExists, "Gamer already exists");

            user = await gamerFactory.Create(binding.Id, HttpContext.GuildId(), binding.Login, binding.Name, binding.DateOfBirth, binding.Rank, binding.Status);

            userRepository.Save(user);

            return Ok(new { });
        }

        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/gamers/{id}/characters")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCharacter(
            [FromRoute] Guid id,
            [FromBody] AddCharacterBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(id, HttpContext.GuildId(), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, "Gamer not found");

            try
            {
                user.AddCharacter(binding.Name, binding.ClassName, binding.IsMain);
            }
            catch (CharacterAlreadyExists e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.CharacterAlreadyExists, e.Message, e.Character.ToDictionary(), e);
            }

            userRepository.Save(user);
            return Ok(new { });
        }

        /// <summary>
        /// Remove character
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpDelete("/gamers/{userId}/characters/{characterId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCharacter(
            [FromRoute] Guid userId,
            [FromRoute] Guid characterId,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(userId, HttpContext.GuildId(), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, "Gamer not found");

            try
            {
                user.CharacterRemove(characterId);
            }
            catch (CharacterNotFound e)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.CharacterNotFound, e.Message);
            }

            userRepository.Save(user);
            return Ok(new { });
        }

        /// <summary>
        /// Remove character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/gamers/current/characters/{characterId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SelfDeleteCharacter(
            [FromRoute] Guid characterId,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(HttpContext.GetUserId(), HttpContext.GuildId(), cancellationToken);
            try
            {
                user.CharacterRemove(characterId);
            }
            catch (CharacterNotFound e)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.CharacterNotFound, e.Message);
            }
            userRepository.Save(user);
            return Ok(new { });
        }

        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/gamers/current/characters")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SelfAddCharacter(
            [FromBody] AddCharacterBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(HttpContext.GetUserId(), HttpContext.GuildId(), cancellationToken);

            try
            {
                user.AddCharacter(binding.Name, binding.ClassName, binding.IsMain);
            }
            catch (CharacterAlreadyExists e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.CharacterAlreadyExists, e.Message, e.Character.ToDictionary(), e);
            }

            userRepository.Save(user);
            return Ok(new { });
        }
    }
}