using System;

namespace Coffers.Public.Queries.Infrastructure.NestContracts.Entity
{
    public sealed class NestContract
    {
        public static String Sql { get; } = "";
        
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