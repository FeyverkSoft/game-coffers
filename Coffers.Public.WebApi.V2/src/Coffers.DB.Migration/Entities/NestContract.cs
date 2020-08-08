using System;
using Coffers.Types.Nest;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class NestContract
    {
        public Guid Id { get; }
        
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }
        public User User { get; }

        public Guid NestId { get; }
        public Nest Nest { get; }
        
        public String CharacterName { get; }

        /// <summary>
        /// Описание награды
        /// </summary>
        public String Reward { get; }

        public DateTime? ExpDate { get; }

        /// <summary>
        /// Состояние контракта
        /// </summary>
        public NestContractStatus Status { get; }

        public Guid ConcurrencyTokens { get; }
    }
}