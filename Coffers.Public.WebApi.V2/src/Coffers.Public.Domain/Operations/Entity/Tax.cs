using System;

namespace Coffers.Public.Domain.Operations.Entity
{
    public sealed class Tax
    {
        /// <summary>
        /// Идентификатор налога
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }
    }
}
