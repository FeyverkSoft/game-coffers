using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Coffers.Public.WebApi.Authorization
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.UserId)).Value);
        }

        public static Guid GetSessionId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }
    }
}
