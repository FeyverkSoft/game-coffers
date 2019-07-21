﻿using System;
using System.Collections.Generic;

namespace Coffers.DB.Migrations.Entities
{
    /// <summary>
    /// Счёт, базовая сущность
    /// </summary>
    internal sealed class Account
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Список операций произведенных над счётом
        /// </summary>
        public List<Operation> Operations { get; set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }

    }
}