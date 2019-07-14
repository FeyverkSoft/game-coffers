using System;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Игровой класс персонажа
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; set; }
    }
}
