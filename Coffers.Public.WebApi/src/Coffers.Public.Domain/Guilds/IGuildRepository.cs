using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Guilds
{
    public interface IGuildRepository
    {
        Task<Guild> UnsafeGet(Guid id, CancellationToken cancellationToken);

        Task<Guild> Get(Guid id, Guid userId, CancellationToken cancellationToken);
        /// <summary>
        /// Подгружает список игроков в объект гильдии
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task LoadGamers(Guild guild, CancellationToken cancellationToken);

        Task Save(Guild guild);
    }
}
