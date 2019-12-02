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
        Task<Session> Get(Guid sessionId, CancellationToken cancellationToken);
        Task Save(Session session);

        /// <summary>
        /// Возвращает информацию о пользователе по его логину
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Gamer> FindGamer(String login, CancellationToken cancellationToken);

        Task<Gamer> GetGamer(Guid userId, CancellationToken cancellationToken);
        Task Save(Gamer gamer);
    }
}
