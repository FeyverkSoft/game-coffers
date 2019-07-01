using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Guilds
{
    public interface IGuildRepository
    {
        Task<Guild> Get(string id, CancellationToken cancellationToken);

        Task Save(Guild guild);
    }
}
