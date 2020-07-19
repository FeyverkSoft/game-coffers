using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.NestContracts
{
    public interface INestGetter
    {
        Task<Nest?> Get(Guid nestId, Guid guildId, CancellationToken cancellationToken);
    }
}