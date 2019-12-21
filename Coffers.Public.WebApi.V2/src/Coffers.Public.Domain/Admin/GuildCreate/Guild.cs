using System;
using Coffers.Types.Guilds;

namespace Coffers.Public.Domain.Admin.GuildCreate
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Название гильдии
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; }

        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; }

        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

        internal Guild() { }

        public Guild(Guid id, String name, GuildStatus status, RecruitmentStatus recruitmentStatus)
        => (Id, Name, Status, RecruitmentStatus)
         = (id, name, status, recruitmentStatus);
    }
}
