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
        public static Guid GuildId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.GuildId)).Value);
        }
        public static Boolean IsAdmin(this HttpContext httpContext)
        {
            return bool.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.Admin)).Value);
        }
        public static Guid GetSessionId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }
    }
}
