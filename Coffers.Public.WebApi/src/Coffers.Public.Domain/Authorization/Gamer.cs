using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; internal set; }

        /// <summary>
        /// Права доступа игрока
        /// </summary>
        public String[] Roles { get; set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; internal set; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get; internal set; }
        /// <summary>
        /// ID гильдии пользователя
        /// </summary>
        public Guid GuildId { get; set; }

        public GamerRank Rank { get; set; }

        internal Gamer() { }

        public void SetPassword(String hash)
        {
            Password = hash;
        }
    }
}
