using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        public SessionController(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        /// <summary>
        /// Авторизирует пользователя в сервисе
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenView), 200)]
        public async Task<IActionResult> Post(
            [FromBody] AuthBinding binding,
            [FromServices] UserSecurityService gamerSecurityService,
            CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.GetUser(binding.Login, cancellationToken);
            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.Forbidden, "");

            if (String.IsNullOrEmpty(gamer.Password))
            {
                gamerSecurityService.CreatePassword(gamer, binding.Password);
                await _authorizationRepository.SaveUser(gamer);
            }
            else
            {
                if (!gamerSecurityService.TestPassword(gamer, binding.Password))
                    throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Forbidden, "");
            }

            var sessionId = Guid.NewGuid();

            await _authorizationRepository.SaveSession(new Session(sessionId, gamer.Id, 60 * 26, HttpContext.GetIp()));

            var roles = new List<String>();
            if (gamer.Roles != null)
                roles.AddRange(gamer.Roles);
            roles.Add(gamer.Rank.ToString().ToLower());
            return Ok(new TokenView
            {
                Token = sessionId,
                GuildId = gamer.GuildId,
                Roles = roles.Distinct(StringComparer.InvariantCultureIgnoreCase).ToArray()
            });
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            var session = await _authorizationRepository.GetSession(HttpContext.GetSessionId(), cancellationToken);
            session.ExtendSession(-1 * 60 * 27);
            await _authorizationRepository.SaveSession(session);
            return Ok(new { });
        }
    }
}