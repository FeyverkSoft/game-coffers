using System;

namespace Coffers.Public.Domain.Operations.Entity
{
    /// <summary>
    /// Сущность хранит займы игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }
    }
}
