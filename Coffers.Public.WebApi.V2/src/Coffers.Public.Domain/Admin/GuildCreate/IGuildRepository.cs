using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Admin.GuildCreate
{
    public interface IGuildRepository
    {
        Task<Guild> Get(Guid id, CancellationToken cancellationToken, Boolean asNonTr = false);

        Task Save(Guild guild);
    }
}
