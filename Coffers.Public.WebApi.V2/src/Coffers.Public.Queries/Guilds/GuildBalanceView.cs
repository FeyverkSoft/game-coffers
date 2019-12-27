using System;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildBalanceView
    {
        public Guid GuildId { get; }
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
        /// Сумма уже уплаченная от суммы займов
        /// </summary>
        public Decimal RepaymentLoansAmount { get; }
        /// <summary>
        /// Балланс игроков на складе гильдии
        /// </summary>
        public Decimal GamersBalance { get; }

        public GuildBalanceView(Guid guildId, Decimal balance, Decimal expectedTaxAmount, Decimal taxAmount, Decimal activeLoansAmount, Decimal repaymentLoansAmount, Decimal gamersBalance)
            => (GuildId, Balance, ExpectedTaxAmount, TaxAmount, ActiveLoansAmount, RepaymentLoansAmount, GamersBalance)
            = (guildId, balance, expectedTaxAmount, taxAmount, activeLoansAmount, repaymentLoansAmount, gamersBalance);

    }
}
