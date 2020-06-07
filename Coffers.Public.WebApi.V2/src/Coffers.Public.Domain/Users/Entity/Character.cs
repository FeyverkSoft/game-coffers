using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users.Entity
{
    public sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

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
        public Boolean IsMain { get; private set; }

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; private set; } = CharStatus.Active;

        public Boolean IsActive => Status == CharStatus.Active;

        public Character(Guid id, String name, String className, Boolean isMain)
        {
            Name = name?.Trim();
            ClassName = className?.Trim();
            IsMain = isMain;
            Id = id;
        }

        /// <summary>
        /// пометить персонажа как удалённый
        /// </summary>
        internal void MarkAsDeleted()
        {
            Status = CharStatus.Deleted;
        }

        /// <summary>
        /// Убрать отметку о том что этот перс основа
        /// </summary>
        internal void UnmarkAsMain()
        {
            IsMain = false;
        }

        /// <summary>
        /// установить отметку о том что этот перс основа
        /// </summary>
        internal void MarkAsMain()
        {
            IsMain = true;
        }
    }
}
