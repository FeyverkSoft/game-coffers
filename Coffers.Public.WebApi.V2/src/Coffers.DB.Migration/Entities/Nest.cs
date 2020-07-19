using System;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Nest
    {
        /// <summary>
        /// Идентификатор логова/инстанса
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// гильдия которая добавила логово/инстанс в список
        /// </summary>
        public Guid GuildId { get; }

        public Guild Guild { get; }

        /// <summary>
        /// Название логова инстанса
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Признак того что логово было убрано/скрыто/удалено
        /// Признак того что логово было убрано/скрыто/удалено
        /// </summary>
        public Boolean IsHidden { get; }
    }
}