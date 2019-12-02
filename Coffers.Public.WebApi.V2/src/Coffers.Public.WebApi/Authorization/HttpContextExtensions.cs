using System;
using System.Linq;
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
        public static String GetIp(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["X-Original-For"].ToString() ??
                   httpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                   httpContext.Request.Headers["X-Real-IP"].ToString();
        }
        public static Guid GuildId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.GuildId)).Value);
        }
        public static Boolean IsAdmin(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst(x => x.Type.Equals(ClaimTypes.Role)).Value?.Split(",").Any(_ => _.Equals("Admin")) == true;
        }
        public static Guid GetSessionId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }
    }
}
