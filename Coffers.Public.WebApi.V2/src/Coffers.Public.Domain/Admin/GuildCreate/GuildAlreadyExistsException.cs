using System;

namespace Coffers.Public.Domain.Admin.GuildCreate
{
    public class GuildAlreadyExistsException : Exception
    {
        public GuildAlreadyExistsException(String message, Exception ex = null) : base(message, ex) { }
    }
}