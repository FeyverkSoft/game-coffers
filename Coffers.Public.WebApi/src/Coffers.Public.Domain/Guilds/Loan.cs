using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Guilds
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        /// Номер счёта по займу
        /// </summary>
        public Account Account { get; set; }


        public LoanStatus LoanStatus { get; set; }
    }


}
