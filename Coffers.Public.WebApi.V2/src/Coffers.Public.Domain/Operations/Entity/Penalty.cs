using System;

namespace Coffers.Public.Domain.Operations.Entity
{
    public sealed class Penalty
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; }

        public Guid UserId { get; }
    }
}
