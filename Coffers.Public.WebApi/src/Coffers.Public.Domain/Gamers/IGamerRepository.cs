using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Gamers
{
    public interface IGamerRepository
    {
        Task<Gamer> Get(Guid userId, CancellationToken none);
        Task Save(Gamer gamer);
        Task Load(Gamer gamer, CancellationToken cancellationToken);
    }
}
