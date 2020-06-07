using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans.Entity;

namespace Coffers.Public.Domain.Loans
{
    public interface IGuildRepository
    {
        Task<Tariff> GetTariff(Guid userId, Guid guildId, CancellationToken cancellationToken);
        Task<Boolean> IsUserExists(Guid userId, Guid guildId, CancellationToken cancellationToken);
    }
}
