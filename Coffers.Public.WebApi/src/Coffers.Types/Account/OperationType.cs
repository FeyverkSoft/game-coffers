﻿namespace Coffers.Types.Account
{
    public enum OperationType
    {
        /// <summary>
        /// Налог
        /// </summary>
        Tax,
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
    }
}