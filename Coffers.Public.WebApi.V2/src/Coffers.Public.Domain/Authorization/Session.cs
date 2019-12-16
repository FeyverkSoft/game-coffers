using System;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class Session
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public Guid SessionId { get; }
        /// <summary>
        /// Игрок которому принадлежит сессия
        /// </summary>
        public Guid UserId { get; }
        public User User { get; }
        /// <summary>
        /// Дата создания сессии
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;
        /// <summary>
        /// Дата стухания сессии
        /// </summary>
        public DateTime ExpireDate { get; private set; }
        /// <summary>
        /// IP адресс с которого была получена сессиия
        /// </summary>
        public String Ip { get; }

        public Boolean IsExpired => ExpireDate <= DateTime.UtcNow;

        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

        public void ExtendSession(Int32 lifetime)
        {
            if (IsExpired)
                throw new InvalidOperationException("Session expired");
            ExpireDate = DateTime.UtcNow.AddMinutes(lifetime);
            ConcurrencyTokens = Guid.NewGuid();
        }

        internal Session() { }

        public Session(Guid sessionId, Guid userId, Int32 lifetime, String ip)
            => (SessionId, UserId, ExpireDate, Ip)
            = (sessionId, userId, DateTime.UtcNow.AddMinutes(lifetime), ip);
    }
}
