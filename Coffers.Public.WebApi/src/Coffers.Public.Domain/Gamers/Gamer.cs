using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; internal set; }


        internal Gamer() { }
    }
}
