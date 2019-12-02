using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    public sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Игровой класс персонажа
        /// </summary>
        public String ClassName { get; private set; }

        /// <summary>
        /// Признак того что это основной перс
        /// </summary>
        public Boolean IsMain { get; private set; } = false;

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; private set; }
    }
}