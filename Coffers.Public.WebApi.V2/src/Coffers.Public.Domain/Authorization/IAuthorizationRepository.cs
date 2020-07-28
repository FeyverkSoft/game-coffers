using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Authorization
{
    /// <summary>
    /// Интерфейс предоставляющий доступ к данным сессий игроков
    /// </summary>
    public interface IAuthorizationRepository
    {
        Task<Session> GetSession(Guid sessionId, CancellationToken cancellationToken);
        Task SaveSession(Session session);

        /// <summary>
        /// Возвращает информацию о пользователе по его логину
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> GetUser(String login, CancellationToken cancellationToken);

        Task<User> GetUser(Guid userId, CancellationToken cancellationToken);
        Task SaveUser(User gamer);
        Task<User> GetUserByEmail(String email, Guid guildId, CancellationToken cancellationToken);
    }
}
