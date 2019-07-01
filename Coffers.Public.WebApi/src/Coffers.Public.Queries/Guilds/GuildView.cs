using System;

namespace Coffers.Public.Queries.Guilds
{
    public class GuildView
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
