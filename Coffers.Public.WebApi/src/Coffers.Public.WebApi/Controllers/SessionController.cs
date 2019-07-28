using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

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
        public async Task<IActionResult> Post([FromBody] AuthBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.FindGamer(binding.Login, cancellationToken);
            if (gamer == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.Forbidden, "");

            if (String.IsNullOrEmpty(gamer.Password))
            {
                gamer = await PutPassword(gamer.Id, binding.Password, cancellationToken);
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(binding.Login + binding.Password + gamer.Id));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            if (!hash.ToString().Equals(gamer.Password, StringComparison.InvariantCultureIgnoreCase))
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Forbidden, "");

            var sessionId = Guid.NewGuid();
            await _authorizationRepository.Save(new Session(sessionId, gamer.Id, 60 * 26, HttpContext.GetIp()));
            var roles = new List<String>();
            if (gamer.Roles != null)
                roles.AddRange(gamer.Roles);
            roles.Add(gamer.Rank.ToString().ToLower());
#warning костыль роли admin
            if (gamer.Login.Equals("Feyverk"))
                roles.Add("Admin");

            return Ok(new TokenView
            {
                Token = sessionId,
                GuildId = gamer.GuildId,
                Roles = roles.Distinct((x, y) => x.Equals(y, StringComparison.InvariantCultureIgnoreCase)).ToArray()
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var session = await _authorizationRepository.Get(HttpContext.GetSessionId(), cancellationToken);
            session.ExtendSession(-1 * 60 * 27);
            await _authorizationRepository.Save(session);
            return Ok(new { });
        }

        /// <summary>
        /// Задаёт логин и пароль пользователю, если у него его нет при первом заходе
        /// Сделать потом автогенеририруемый с отправкой на почту.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Gamer> PutPassword(Guid userId, String password, CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.GetGamer(userId, cancellationToken);
            if (gamer == null || gamer.Id != userId || !string.IsNullOrEmpty(gamer.Password))
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.Forbidden, "");

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(gamer.Login + password + gamer.Id));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            gamer.SetPassword(hash.ToString());
            await _authorizationRepository.Save(gamer);
            return gamer;
        }
    }
}