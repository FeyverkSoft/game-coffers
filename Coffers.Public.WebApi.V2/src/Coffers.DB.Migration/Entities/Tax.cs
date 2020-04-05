using System;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class  Tax
    {
        /// <summary>
        /// Идентификатор налога
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// игрок
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Тариф по которому проходит налог
        /// </summary>
        public String TaxTariff { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Сумма налога
        /// </summary>
        public Decimal Amount { get; }

        public TaxStatus Status { get; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросов
        /// </summary>
        public Guid ConcurrencyTokens { get; }
    }
}
