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
        public Guid Id { get; }

        public List<Operation> Operations { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; }

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

        /// <summary>
        /// Список игроков в гильдии
        /// </summary>
        public ICollection<User> Users { get; }

        /// <summary>
        /// Список ролей игроков в гильдии
        /// </summary>
        public ICollection<UserRole> Roles { get; }
        public Guid ConcurrencyTokens { get; }

        protected Guild() { }
        public Guild(Guid id, RecruitmentStatus recruitmentStatus, string name, DateTime createDate, DateTime updateDate, GuildStatus status)
            => (Id, RecruitmentStatus, Name, CreateDate, UpdateDate, Status) 
            = (id, recruitmentStatus, name, createDate, updateDate, status);
    }
}
