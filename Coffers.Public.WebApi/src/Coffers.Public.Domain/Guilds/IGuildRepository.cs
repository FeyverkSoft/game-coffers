using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Guilds
{
    public interface IGuildRepository
    {
        Task<Guild> Get(Guid id, CancellationToken cancellationToken);
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
