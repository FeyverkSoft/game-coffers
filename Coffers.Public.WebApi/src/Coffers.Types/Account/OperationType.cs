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
        /// Награда
        /// </summary>
        Reward,
        /// <summary>
        /// ЗП
        /// </summary>
        Salary,
        /// <summary>
        /// Эмиссия игровых средств в систему
        /// </summary>
        Emission,
        /// <summary>
        /// Иное
        /// </summary>
        Other
    }
}
