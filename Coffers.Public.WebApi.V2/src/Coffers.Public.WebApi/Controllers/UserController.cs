using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.Domain.Users;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffers.Helpers;
using Coffers.Public.Queries.Users;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class UserController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/guilds/current/gamers")]
        [ProducesResponseType(201)]
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

            user = await gamerFactory.Create(binding.Id, HttpContext.GetGuildId(), binding.Login, binding.Name, binding.DateOfBirth, binding.Rank, binding.Status);

            userRepository.Save(user);

            return Ok(new { });
        }

        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/gamers/{id}/characters")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> AddCharacter(
            [FromRoute] Guid id,
            [FromBody] AddCharacterBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(id, HttpContext.GetGuildId(), cancellationToken);

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
        /// Get profile
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/gamers/current/profile")]
        [ProducesResponseType(typeof(ProfileView), 200)]
        public async Task<IActionResult> GetProfile(
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            return Ok(await queryProcessor.Process<ProfileViewQuery, ProfileView>(new ProfileViewQuery(
                HttpContext.GetUserId(),
                HttpContext.GetGuildId()), cancellationToken));
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/gamers/current/characters")]
        [ProducesResponseType(typeof(IEnumerable<CharacterView>), 200)]
        public async Task<IActionResult> GetCharacters(
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            return Ok(await queryProcessor.Process<CharacterViewQuery, IEnumerable<CharacterView>>(new CharacterViewQuery(
                HttpContext.GetUserId(),
                HttpContext.GetGuildId()), cancellationToken));
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
            var user = await userRepository.Get(userId, HttpContext.GetGuildId(), cancellationToken);

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
            var user = await userRepository.Get(HttpContext.GetUserId(), HttpContext.GetGuildId(), cancellationToken);
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
        [ProducesResponseType(201)]
        public async Task<IActionResult> SelfAddCharacter(
            [FromBody] AddCharacterBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(HttpContext.GetUserId(), HttpContext.GetGuildId(), cancellationToken);

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
        /// Change user rank
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPatch("/gamers/{userId}/rank")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> ChangeRank(
            [FromRoute] Guid userId,
            [FromBody] ChangeRankBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(userId, HttpContext.GetGuildId(), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, "Gamer not found");

            user.ChangeRank(binding.Rank);

            userRepository.Save(user);
            return Ok(new { });
        }

        /// <summary>
        /// Change user status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPatch("/gamers/{userId}/status")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangeStatus(
            [FromRoute] Guid userId,
            [FromBody] ChangeStatusBinding binding,
            [FromServices] Domain.Users.IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(userId, HttpContext.GetGuildId(), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, "Gamer not found");

            user.ChangeStatus(binding.Status);

            userRepository.Save(user);
            return Ok(new { });
        }
    }
}