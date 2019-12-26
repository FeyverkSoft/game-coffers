using System;
using Coffers.Types.Guilds;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildView
    {
        public Guid Id { get; }
        public String Name { get; }
        public RecruitmentStatus RecruitmentStatus { get; }
        public Int32 GamersCount { get; }
        public Int32 CharactersCount { get; }
        GuildView() { }
        public GuildView(Guid id, String name, RecruitmentStatus recruitmentStatus,
           Int32 gamersCount, Int32 charactersCount) =>
            (Id, Name, RecruitmentStatus, GamersCount, CharactersCount) =
            (id, name, recruitmentStatus, gamersCount, charactersCount);
    }
}