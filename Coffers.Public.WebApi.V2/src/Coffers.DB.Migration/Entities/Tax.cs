using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class  Tax
    {
        /// <summary>
        /// Идентификатор налога
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// игрок
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тариф по которому проходит налог
        /// </summary>
        public String TaxTariff { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Сумма налога
        /// </summary>
        public Decimal Amount { get; set; }

        public List<Operation> Operations { get; set; }

        public TaxStatus Status { get; set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросов
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }
    }
}
