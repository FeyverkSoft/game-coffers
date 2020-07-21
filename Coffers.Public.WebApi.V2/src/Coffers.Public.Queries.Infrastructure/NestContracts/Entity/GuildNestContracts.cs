using System;
using System.Collections.Generic;
using System.Text;

namespace Coffers.Public.Queries.Infrastructure.NestContracts.Entity
{
    internal sealed class GuildNestContracts
    {
        public static String Sql { get; } = @"
select 
  nc.`Id` as 'Id',
  nc.`UserId` as 'UserId',
  n.`Name` as 'NestName',
  nc.`Name` as 'CharacterName',
  nc.`Reward` as 'Reward'
from `NestContract` nc 
join `Nest` n on n.Id = nc.NestId 
where 1 = 1 
and n.`GuildId` = @GuildId
";

        public Guid Id { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Название логова
        /// </summary>
        public String NestName { get; }

        /// <summary>
        /// Ник чара
        /// </summary>
        public String CharacterName { get; }

        /// <summary>
        /// Описание награды
        /// </summary>
        public String Reward { get; }
    }
}
