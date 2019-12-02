using System;

namespace Coffers.Public.Queries.Infrastructure.Operations
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Номер счёта по займу
        /// </summary>
        public Account Account { get; set; }

    }
}
