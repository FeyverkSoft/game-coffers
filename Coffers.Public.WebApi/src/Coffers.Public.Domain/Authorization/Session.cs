using System;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class Session
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public Guid SessionId { get; private set; }
        /// <summary>
        /// Игрок которому принадлежит сессия
        /// </summary>
        public Guid GamerId { get; private set; }
        /// <summary>
        /// Дата создания сессии
        /// </summary>
        public DateTime CreateDate { get; private set; }
        /// <summary>
        /// Дата стухания сессии
        /// </summary>
        public DateTime ExpireDate { get; private set; }
        /// <summary>
        /// IP адресс с которого была получена сессиия
        /// </summary>
        public String Ip { get; private set; }

        public Boolean IsExpired => ExpireDate <= DateTime.UtcNow;
        public void ExtendSession(Int32 lifetime)
        {
            ExpireDate = DateTime.UtcNow.AddMinutes(lifetime);
        }

        internal Session() { }

        public Session(Guid sessionId, Guid gamerId, Int32 lifetime, String ip)
        {
            SessionId = sessionId;
            GamerId = gamerId;
            Ip = ip;
            ExpireDate = DateTime.UtcNow.AddMinutes(lifetime);
            CreateDate = DateTime.UtcNow;
        }
    }
}
