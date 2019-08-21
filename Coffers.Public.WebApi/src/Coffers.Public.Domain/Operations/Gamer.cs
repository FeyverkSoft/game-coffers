using System;

namespace Coffers.Public.Domain.Operations
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; private set; }
    }
}
