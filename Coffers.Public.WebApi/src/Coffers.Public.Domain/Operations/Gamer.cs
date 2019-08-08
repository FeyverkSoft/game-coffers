using System;

namespace Coffers.Public.Domain.Operations
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; internal set; }
    }
}
