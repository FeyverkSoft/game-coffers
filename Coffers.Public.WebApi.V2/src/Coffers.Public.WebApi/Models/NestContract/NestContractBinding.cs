using System;

namespace Coffers.Public.WebApi.Models.NestContract
{
    public sealed class NestContractBinding
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор логова
        /// </summary>
        public Guid NestId { get; set; }

        /// <summary>
        /// Имя перса на котором контракт
        /// </summary>
        public String CharacterName { get;  set;}

        /// <summary>
        /// Награда
        /// </summary>
        public String Reward { get; set; }
    }
}