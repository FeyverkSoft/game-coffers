using System;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class Nest
    {
        /// <summary>
        /// Идентификатор логова/инстанса
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// гильдия которая добавила логово/инстанс в список
        /// </summary>
        public Guid GuildId { get; }

        /// <summary>
        /// Признак того что логово было убрано/скрыто/удалено
        /// </summary>
        public Boolean IsHidden { get; }
    }
}