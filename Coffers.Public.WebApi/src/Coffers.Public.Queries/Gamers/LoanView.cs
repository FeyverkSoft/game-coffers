using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class LoanView
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Дата когда был взят займ
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Статус займа
        /// </summary>
        public LoanStatus LoanStatus { get; set; }
    }
}
