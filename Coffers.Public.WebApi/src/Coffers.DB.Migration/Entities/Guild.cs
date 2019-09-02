using Coffers.Types.Guilds;
using System;
using System.Collections.Generic;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// тариф гильдии, действующий на данный момент
        /// </summary>
        public GuildTariff Tariff { get; set; }

        /// <summary>
        /// Счёт гильдии
        /// </summary>
        public Account GuildAccount { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Название гильдии
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; set; }

        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; set; }

        /// <summary>
        /// Список игроков в гильдии
        /// </summary>
        public ICollection<Gamer> Gamers { get; set; }

        /// <summary>
        /// Опер дни
        /// </summary>
        public ICollection<OperDay> OperDays { get; set; }
    }
}
