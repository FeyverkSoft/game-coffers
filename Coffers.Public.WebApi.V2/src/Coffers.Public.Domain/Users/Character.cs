using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users
{
    public sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Игровой класс персонажа
        /// </summary>
        public String ClassName { get; }

        /// <summary>
        /// Признак того что это основной перс
        /// </summary>
        public Boolean IsMain { get; }

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; }
    }
}
