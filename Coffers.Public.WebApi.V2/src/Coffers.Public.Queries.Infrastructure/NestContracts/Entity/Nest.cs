using System;

namespace Coffers.Public.Queries.Infrastructure.NestContracts.Entity
{
    internal sealed class Nest
    {
        public static String Sql { get; } = @"
select * from `Nest` n 
where 1 = 1 
and `GuildId` = @GuildId
and `IsHidden` = 0";
        
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