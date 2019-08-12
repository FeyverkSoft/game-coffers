using System;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class GamerInfoView
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// Идентификатор счёта пользователя
        /// </summary>
        public Guid AccountId { get; private set; }
        /// <summary>
        /// Идентификатор гильдии которой принадлежит пользователь
        /// </summary>
        public Guid GuildId { get; private set; }

        public GamerInfoView(Guid userId, Guid accountId, Guid guildId)
        {
            UserId = userId;
            AccountId = accountId;
            GuildId = guildId;
        }
    }
}
