using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.NestContracts
{
    public interface INestContractRepository
    {
        Task<NestContract?> Get(Guid id, CancellationToken cancellationToken);
        Task Save(NestContract loan, CancellationToken cancellationToken);
    }
}