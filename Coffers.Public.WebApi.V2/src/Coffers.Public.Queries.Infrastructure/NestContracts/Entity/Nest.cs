using System;

namespace Coffers.Public.Queries.Infrastructure.NestContracts.Entity
{
    public sealed class Nest
    {
        public static String Sql { get; } = "";
        
        /// <summary>
        /// Идентификатор логова/инстанса
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название логова
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Признак того что логово было убрано/скрыто/удалено
        /// </summary>
        public Boolean IsHidden { get; }
    }
}