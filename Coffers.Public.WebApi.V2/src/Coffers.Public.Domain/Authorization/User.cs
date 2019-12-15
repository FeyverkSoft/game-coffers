using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; private set; }

        /// <summary>
        /// Права доступа игрока
        /// </summary>
        public String[] Roles { get; private set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; private set; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get; private set; }
        /// <summary>
        /// ID гильдии пользователя
        /// </summary>
        public Guid GuildId { get; private set; }

        public GamerRank Rank { get; private set; }

        internal User() { }

        public void SetPassword(String hash)
        {
            if (String.IsNullOrEmpty(hash))
                throw new ArgumentException($"Argument: {nameof(hash)} cannot be null or empty.");
            Password = hash;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
