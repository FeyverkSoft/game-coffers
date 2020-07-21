using System;
using System.Collections.Generic;
using System.Text;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class GuildNestContractView
    {
        public Guid Id { get; }

        /// <summary>
        /// Ник чара
        /// </summary>
        public String CharacterName { get; }

        /// <summary>
        /// Описание награды
        /// </summary>
        public String Reward { get; }

        public GuildNestContractView(Guid id, String characterName, String reward) =>
            (Id, CharacterName, Reward) =
            (id, characterName, reward);
    }
}