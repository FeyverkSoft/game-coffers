using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
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
                return AuthenticateResult.NoResult();

            if (!Guid.TryParse(authorization.Substring("Bearer ".Length).Trim(), out var sessionId))
                return AuthenticateResult.Fail("Session not found");

            var session = await _authorizationRepository.Get(sessionId, CancellationToken.None);

            if (session == null)
                return AuthenticateResult.Fail("Session not found");

            if (session.IsExpired)
                return AuthenticateResult.Fail("Session expired");

            session.ExtendSession(60 * 26);

            await _authorizationRepository.Save(session);

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, session.SessionId.ToString(), ClaimValueTypes.String),
                new Claim(ClaimType.UserId, session.GamerId.ToString(), ClaimValueTypes.String),
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Token"));
            var authTicket = new AuthenticationTicket(principal, null, "Token");
            return AuthenticateResult.Success(authTicket);
        }
    }
}
