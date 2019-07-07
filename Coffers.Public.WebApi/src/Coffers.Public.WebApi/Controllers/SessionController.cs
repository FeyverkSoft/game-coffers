using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
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
        public async Task<IActionResult> Post([FromBody] AuthBinding binding, CancellationToken cancellationToken)
        {
            var gamer = await _authorizationRepository.FindGamer(binding.Login, cancellationToken);
            if (gamer == null || String.IsNullOrEmpty(gamer.Password))
                throw new ApiException(HttpStatusCode.NotFound, "not_found", "");

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(binding.Login + binding.Password + gamer.Id));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            if (!hash.ToString().Equals(gamer.Password, StringComparison.InvariantCultureIgnoreCase))
                throw new ApiException(HttpStatusCode.Unauthorized, "not_found", "");

            var sessionId = Guid.NewGuid();
            var ipAddress = HttpContext.Request.Headers["X-Original-For"].ToString() ??
                            HttpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                            HttpContext.Request.Headers["X-Real-IP"].ToString();
            await _authorizationRepository.Save(new Session(sessionId, gamer.Id, 60 * 26, ipAddress));

            return Ok(new TokenView
            {
                Token = sessionId,
            });
        }
    }
}