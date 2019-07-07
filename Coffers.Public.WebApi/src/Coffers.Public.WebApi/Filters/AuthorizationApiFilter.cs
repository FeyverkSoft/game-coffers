using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coffers.Public.WebApi.Filters
{
    public class AuthorizationApiFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
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
