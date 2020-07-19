using System;

namespace Coffers.Public.WebApi.Models.NestContract
{
    public sealed class NestContractBinding
    {
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор логова
        /// </summary>
        public Guid NestId { get; }

        /// <summary>
        /// Имя перса на котором контракт
        /// </summary>
        public String CharacterName { get; }

        /// <summary>
        /// Награда
        /// </summary>
        public String Reward { get; }
    }
}