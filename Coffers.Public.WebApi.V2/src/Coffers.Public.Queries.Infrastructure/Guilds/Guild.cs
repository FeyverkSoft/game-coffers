using System;
using System.Collections.Generic;
using Coffers.Types.Guilds;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Счёт гильдии
        /// </summary>
        public Account GuildAccount { get; private set; }

        /// <summary>
        /// Название гильдии
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// тариф гильдии, действующий на данный момент
        /// </summary>
        public GuildTariff Tariff { get; private set; }

        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; private set; }

        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; private set; }

        /// <summary>
        /// Список игроков в гильдии
        /// </summary>
        public ICollection<Gamer> Gamers { get; private set; }
    }
}
