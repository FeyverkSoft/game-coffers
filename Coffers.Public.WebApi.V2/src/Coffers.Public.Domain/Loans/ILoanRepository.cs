using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public interface ILoanRepository
    {
        Task<Loan> Get(Guid id, Guid guildId, CancellationToken cancellationToken);

        Task Save(Loan user);
    }
}
