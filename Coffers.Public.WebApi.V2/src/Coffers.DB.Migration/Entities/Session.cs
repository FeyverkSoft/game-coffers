using System;

namespace Coffers.DB.Migrations.Entities
{
    internal class Session
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public Guid SessionId { get; }
        /// <summary>
        /// Игрок которому принадлежит сессия
        /// </summary>
        public Guid UserId { get; }
        public User User { get; }
        /// <summary>
        /// Дата создания сессии
        /// </summary>
        public DateTime CreateDate { get; }
        /// <summary>
        /// Дата стухания сессии
        /// </summary>
        public DateTime ExpireDate { get; }
        /// <summary>
        /// IP адресс с которого была получена сессиия
        /// </summary>
        public String Ip { get; }
        public Guid ConcurrencyTokens { get; }
    }
}
