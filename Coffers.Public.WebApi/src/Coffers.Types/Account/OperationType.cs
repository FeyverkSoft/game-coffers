namespace Coffers.Types.Account
{
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
        /// Иное
        /// </summary>
        Other,
        /// <summary>
        /// Эмиссия игровых средств в систему
        /// </summary>
        Emission
    }
}
