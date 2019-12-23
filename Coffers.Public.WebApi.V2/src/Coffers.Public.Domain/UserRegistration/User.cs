using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }
        public Guid GuildId { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; }

        public Guid ConcurrencyTokens { get; set; } = Guid.NewGuid();
        internal User() { }

        public User(Guid id, Guid guildId, string name, GamerRank rank, GamerStatus status, DateTime dateOfBirth, string login)
            => (Id, GuildId, Name, Rank, Status, DateOfBirth, Login)
                = (id, guildId, name.Trim(), rank, status, dateOfBirth, login.Trim());
    }
}
