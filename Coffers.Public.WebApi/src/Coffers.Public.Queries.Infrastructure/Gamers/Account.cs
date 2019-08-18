﻿using System;
using System.Collections.Generic;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    public sealed class Account
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public Decimal Balance { get; private set; }
    }
}