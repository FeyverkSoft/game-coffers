using System;

namespace Coffers.Public.Queries.Guilds
{
    /// <summary>
    /// Информация о балансе гильдии
    /// </summary>
    public sealed class GuildBalanceView
    {
        /// <summary>
        /// Баланс гильдии на момент запроса
        /// </summary>
        public Decimal Balance { get; set; }
        /// <summary>
        /// Ожидаемая сумма налога, на конец месяца
        /// </summary>
        public Decimal ExpectedTaxAmount { get; set; }
        /// <summary>
        /// Уплаченная сумма налогов на текущий момент
        /// </summary>
        public Decimal TaxAmount { get; set; }
        /// <summary>
        /// Сумма активных займов
        /// </summary>
        public Decimal ActiveLoansAmount { get; set; }
        /// <summary>
        /// Балланс игроков на складе гильдии
        /// </summary>
        public Decimal GamersBalance { get; set; }

        /// <summary>
        /// Сумма уже уплаченная от суммы займов
        /// </summary>
        public Decimal RepaymentLoansAmount { get; set; }
    }
}
