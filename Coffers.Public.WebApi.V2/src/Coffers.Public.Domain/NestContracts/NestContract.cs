using System;

using Coffers.Types.Nest;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class NestContract
    {
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }

        public Guid NestId { get; }

        public DateTime? ExpDate { get; private set; }

        public String CharacterName { get; }

        /// <summary>
        /// Описание награды
        /// </summary>
        public String Reward { get; }

        /// <summary>
        /// Состояние контракта
        /// </summary>
        public NestContractStatus Status { get; private set; } = NestContractStatus.Active;

        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

        protected NestContract() { }

        public NestContract(Guid id, Guid userId, Guid nestId, String characterName, String reward)
        {
            Id = id;
            UserId = userId;
            NestId = nestId;
            CharacterName = characterName;
            Reward = reward;
        }

        public void Close()
        {
            Status = NestContractStatus.Closed;
            ConcurrencyTokens = Guid.NewGuid();
        }

        public void SetTimeOut(Int32 hours)
        {
            if (hours <= 0)
                throw new ArgumentOutOfRangeException(nameof(hours));

            ExpDate = DateTime.UtcNow.AddHours(hours);
            ConcurrencyTokens = Guid.NewGuid();
        }

        public void MarkAsExpire()
        {
            if (Status == NestContractStatus.Closed)
                return;
            if (ExpDate == null || ExpDate >= DateTime.UtcNow)
                return;
            Status = NestContractStatus.Closed;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}