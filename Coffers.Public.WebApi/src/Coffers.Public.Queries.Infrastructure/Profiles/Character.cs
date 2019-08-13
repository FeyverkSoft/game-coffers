using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Profiles
{
    public sealed class Character
    {
        /// <summary>
        /// Идентификатор персонажа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Статус персонажа
        /// </summary>
        public CharStatus Status { get; private set; }
    }
}