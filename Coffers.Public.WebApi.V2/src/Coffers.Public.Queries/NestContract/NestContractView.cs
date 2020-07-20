using System;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class NestContractView
    {
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

        public NestContractView(Guid id, Guid userId, String nestName, String characterName, String reward)
            => (Id, UserId, NestName, CharacterName, Reward)
                = (id, userId, nestName, characterName, reward);
    }
}