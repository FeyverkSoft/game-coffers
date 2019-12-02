using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
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
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; private set; }

        /// <summary>
        /// Признак основы
        /// </summary>
        public Boolean IsMain { get; private set; } = false;

        public Character(Guid id, CharStatus status, string name, string className, Boolean isMain) => (Id, Status, Name, ClassName, IsMain) = (id, status, name, className, isMain);

        public void Ban()
        {
            if (Status != CharStatus.Deleted)
                Status = CharStatus.Deleted;
        }

        public void MarkAsDeleted()
        {
            Status = CharStatus.Deleted;
        }

        public void MarkAsActive()
        {
            Status = CharStatus.Active;
        }
    }
}