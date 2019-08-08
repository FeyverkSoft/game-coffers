using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; private set; }

        public LoanStatus LoanStatus { get; private set; }
    }


}
