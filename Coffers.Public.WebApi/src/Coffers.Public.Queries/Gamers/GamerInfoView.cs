using System;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class GamerInfoView
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public Guid GuildId { get; set; }
    }
}
