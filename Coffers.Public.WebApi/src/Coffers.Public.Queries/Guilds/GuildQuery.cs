using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    public class GuildQuery : IQuery<GuildView>
    {
        public string Id { get; set; }
    }
}
