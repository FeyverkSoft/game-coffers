using System;

namespace Coffers.Public.WebApi.Authorization
{
    public class ClaimType
    {
        public const String UserId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Coffers.UserId";
        public const String GuildId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Coffers.GuildId";
        public const String Admin = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Coffers.Admin";
    }
}
