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
        public decimal Balance { get; set; }
        /// <summary>
        /// Ожидаемая сумма налога, на конец месяца
        /// </summary>
        public decimal ExpectedTaxAmount { get; set; }
        /// <summary>
        /// Уплаченная сумма налогов на текущий момент
        /// </summary>
        public decimal TaxAmount { get; set; }
        /// <summary>
        /// Сумма активных займов
        /// </summary>
        public decimal ActiveLoansAmount { get; set; }
    }
}
