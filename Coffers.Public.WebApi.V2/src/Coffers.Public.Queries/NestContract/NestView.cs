using System;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class NestView
    {
        public Guid Id { get; }
        public Guid GuildId { get; }
        public String Name { get; }

        public NestView(Guid id, Guid guildId, String name) =>
            (Id, GuildId, Name) =
            (id, guildId, name);
    }
}