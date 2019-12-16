﻿using System;

namespace Coffers.DB.Migrations.Entities
{
    internal class Session
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public Guid SessionId { get; private set; }
        /// <summary>
        /// Игрок которому принадлежит сессия
        /// </summary>
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        /// <summary>
        /// Дата создания сессии
        /// </summary>
        public DateTime CreateDate { get; private set; }
        /// <summary>
        /// Дата стухания сессии
        /// </summary>
        public DateTime ExpireDate { get; private set; }
        /// <summary>
        /// IP адресс с которого была получена сессиия
        /// </summary>
        public String Ip { get; private set; }
        public Guid ConcurrencyTokens { get; set; }
    }
}
