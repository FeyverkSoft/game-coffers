using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coffers.Public.WebApi.Filters
{
    public class AuthorizationApiFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.All(f => f.GetType() != typeof(AuthorizeFilter)) ||
                context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter)))
            {
                return;
            }

            var user = context.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
