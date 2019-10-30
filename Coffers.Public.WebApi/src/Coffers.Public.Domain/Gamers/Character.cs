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

        public Character(Guid id, CharStatus status, string name, string className) => (Id, Status, Name, ClassName) = (id, status, name, className);

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