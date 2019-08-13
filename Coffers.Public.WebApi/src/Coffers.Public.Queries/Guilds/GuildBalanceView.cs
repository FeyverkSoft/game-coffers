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
        public Decimal Balance { get; }
        /// <summary>
        /// Ожидаемая сумма налога, на конец месяца
        /// </summary>
        public Decimal ExpectedTaxAmount { get; }
        /// <summary>
        /// Уплаченная сумма налогов на текущий момент
        /// </summary>
        public Decimal TaxAmount { get; }
        /// <summary>
        /// Сумма активных займов
        /// </summary>
        public Decimal ActiveLoansAmount { get; }
        /// <summary>
        /// Балланс игроков на складе гильдии
        /// </summary>
        public Decimal GamersBalance { get; }

        /// <summary>
        /// Сумма уже уплаченная от суммы займов
        /// </summary>
        public Decimal RepaymentLoansAmount { get; }

        public GuildBalanceView(
            Decimal balance,
            Decimal expectedTaxAmount,
            Decimal taxAmount,
            Decimal activeLoansAmount,
            Decimal gamersBalance,
            Decimal repaymentLoansAmount)
        {
            Balance = balance;
            ExpectedTaxAmount = expectedTaxAmount;
            TaxAmount = taxAmount;
            ActiveLoansAmount = activeLoansAmount;
            GamersBalance = gamersBalance;
            RepaymentLoansAmount = repaymentLoansAmount;
        }
    }
}
