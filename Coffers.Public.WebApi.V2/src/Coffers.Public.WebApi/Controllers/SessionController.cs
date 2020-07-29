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
            [FromBody] AuthLoginBinding binding,
            [FromServices] UserSecurityService gamerSecurityService,
            CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.GetUser(binding.Login, cancellationToken);
            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.Forbidden, "");

            if (String.IsNullOrEmpty(gamer.Password)){
                gamerSecurityService.CreatePassword(gamer, binding.Password);
                await _authorizationRepository.SaveUser(gamer);
            }
            else{
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

        /// <summary>
        /// Авторизирует пользователя в сервисе по емайлу
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("byemail")]
        [ProducesResponseType(typeof(TokenView), 200)]
        public async Task<IActionResult> LoginByEmail(
            [FromBody] AuthEmailBinding binding,
            [FromServices] UserSecurityService gamerSecurityService,
            CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.GetUserByEmail(binding.Email, binding.GuildId, cancellationToken);
            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.Forbidden, "");

            if (!gamer.IsActive){
                throw new ApiException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, "");
            }

            if (!gamerSecurityService.TestPassword(gamer, binding.Password))
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Forbidden, "");


            var sessionId = Guid.NewGuid();
            await _authorizationRepository.SaveSession(new Session(sessionId, gamer.Id, 60 * 26, HttpContext.GetIp()));
            var roles = new List<String>();
            if (gamer.Roles != null)
                roles.AddRange(gamer.Roles);

            // костыль для демо интрефейса :D, надо будет отрефакторить...
            if (binding.GuildId == Guid.Parse("00000000-0000-4000-0000-000000000003"))
                roles.Add("Demo");

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