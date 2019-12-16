using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coffers.Public.WebApi.Filters
{
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly String[] _roles;

        private readonly IAuthorizationRepository _authorizationRepository;

        public PermissionAuthorizationFilter(IAuthorizationRepository authorizationRepository, String[] roles)
        {
            _authorizationRepository = authorizationRepository;
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Method.Equals(HttpMethod.Options.Method))
                return;

            var userId = context.HttpContext.GetUserId();

            var gamer = await _authorizationRepository.GetUser(userId, CancellationToken.None);

            /* if (user.IsGod)
                 return;*/
            if (!_roles.Any())
                return;

            if (gamer.Roles != null)
            {
                if (_roles.Any(role => gamer.Roles.Contains(role)))
                {
                    return;
                }
            }

            if (_roles.Any(role => gamer.Rank.ToString().Equals(role, StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            context.Result = new ObjectResult(new ProblemDetails
            {
                Type = ErrorCodes.Forbidden,
                Detail = "access denied",
                Status = (Int32)HttpStatusCode.Forbidden,
            })
            {
                StatusCode = (Int32)HttpStatusCode.Forbidden
            };
        }
    }
}
