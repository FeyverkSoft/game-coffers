using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users
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
        public Boolean IsMain { get; }

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; private set; } = CharStatus.Active;

        public Boolean IsActive => Status == CharStatus.Active;

        public Character(String name, String className, Boolean isMain)
        {
            Name = name?.Trim();
            ClassName = className?.Trim();
            IsMain = isMain;
        }

        internal void MarkAsDeleted()
        {
            Status = CharStatus.Deleted;
        }
    }
}
