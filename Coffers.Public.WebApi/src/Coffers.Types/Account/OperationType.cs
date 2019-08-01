namespace Coffers.Types.Account
{
    /// <summary>
    /// Тип доступных операций
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Налог
        /// </summary>
        Tax,
        /// <summary>
        /// Продажа в пользу ги
        /// </summary>
        Sell,
        /// <summary>
        /// Штраф
        /// </summary>
        Penalty,
        /// <summary>
        /// Займ
        /// </summary>
        Loan,
        /// <summary>
        /// Обмен между своими персонажами
        /// </summary>
        Exchange,
        /// <summary>
        /// Эмиссия игровых средств в систему
        /// </summary>
        Emission,
        /// <summary>
        /// Операция вывод средств из гильдии во внешнюю систему в пользу игрока
        /// Без зачисления на гильдиский счёт игрока
        /// </summary>
        Output,
        /// <summary>
        /// Иное
        /// </summary>
        Other
    }
}
