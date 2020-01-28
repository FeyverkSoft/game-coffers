using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public interface IGuildRepository
    {
        Task<Tariff> GetTariff(Guid guildId, CancellationToken cancellationToken);
    }
}
