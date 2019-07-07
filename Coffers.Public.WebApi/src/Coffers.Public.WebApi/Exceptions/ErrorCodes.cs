using System;

namespace Coffers.Public.WebApi.Exceptions
{
    public static class ErrorCodes
    {
        public const String InternalServerError = "internal_server_error";
        public const String GuildNotFound = "guild_not_found";
        public const String GuildAlreadyExists = "guild_already_exists";
        public const String GamerAlreadyExists = "gamer_already_exists";
        public const String Forbidden = "forbidden";
    }
}
