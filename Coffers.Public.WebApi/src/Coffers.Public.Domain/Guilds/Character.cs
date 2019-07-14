using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; set; }
    }
}