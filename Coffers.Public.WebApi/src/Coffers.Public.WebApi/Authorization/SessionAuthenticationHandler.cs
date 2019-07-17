using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Coffers.Public.WebApi.Authorization
{
    public class SessionAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public SessionAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthorizationRepository authorizationRepository)
            : base(options, logger, encoder, clock)
        {
            _authorizationRepository = authorizationRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorization = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization))
                return AuthenticateResult.NoResult();

            if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Unauthorized, "Session not found");

#warning //костыль для первой инициализации проекта. Пока что нет админки
            if (authorization.Equals("Bearer G$Ujf%Oz@ZMXRobN"))
            {
                return AuthenticateResult.Success(new AuthenticationTicket(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString(), ClaimValueTypes.String),
                            new Claim(ClaimType.Admin, true.ToString(), ClaimValueTypes.Boolean),
                        }, "Token")),
                    null,
                    "Token"));
            }

            if (!Guid.TryParse(authorization.Substring("Bearer ".Length).Trim(), out var sessionId))
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Unauthorized, "Session not found");

            var session = await _authorizationRepository.Get(sessionId, CancellationToken.None);

            if (session == null)
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Unauthorized, "Session not found");

            if (session.IsExpired)
                throw new ApiException(HttpStatusCode.Unauthorized, ErrorCodes.Unauthorized, "Session expired");

            session.ExtendSession(60 * 26);

            await _authorizationRepository.Save(session);

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, session.SessionId.ToString(), ClaimValueTypes.String),
                new Claim(ClaimType.UserId, session.Gamer.Id.ToString(), ClaimValueTypes.String),
                new Claim(ClaimType.GuildId, session.Gamer.GuildId.ToString(), ClaimValueTypes.String),

#warning //костыль для определения что это админ сервера
                new Claim(ClaimType.Admin, (session.Gamer.Login=="Feyverk").ToString(), ClaimValueTypes.Boolean),
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Token"));
            var authTicket = new AuthenticationTicket(principal, null, "Token");
            return AuthenticateResult.Success(authTicket);
        }
    }
}
