using System;
using System.Collections.Generic;

using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; }

        /// <summary>
        /// Права доступа игрока
        /// </summary>
        public IEnumerable<String> Roles { get; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get; private set; }
        /// <summary>
        /// ID гильдии пользователя
        /// </summary>
        public Guid GuildId { get; }

        public GamerRank Rank { get; }
        public Guid ConcurrencyTokens { get; } = Guid.NewGuid();
        public String? Email { get; set; }

        public Boolean IsActive =>
            Status == GamerStatus.Active ||
            Status == GamerStatus.Afk ||
            Status == GamerStatus.Spirit;

        internal User() { }

        internal void SetPassword(String hash)
        {
            if (String.IsNullOrEmpty(hash))
                throw new ArgumentException($"Argument: {nameof(hash)} cannot be null or empty.");
            Password = hash;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
