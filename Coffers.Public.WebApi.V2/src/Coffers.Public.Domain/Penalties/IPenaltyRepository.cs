using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties.Entity;

namespace Coffers.Public.Domain.Penalties
{
    public interface IPenaltyRepository
    {
        Task<Penalty> Get(Guid id, CancellationToken cancellationToken);
        Task<Penalty> Get(Guid id, Guid userId, CancellationToken cancellationToken);
        Task Save(Penalty penalty);
    }
}
