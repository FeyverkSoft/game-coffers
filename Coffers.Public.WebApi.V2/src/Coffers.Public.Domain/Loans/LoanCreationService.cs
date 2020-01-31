using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanCreationService
    {
        private readonly IGuildRepository _guildRepository;

        public LoanCreationService(IGuildRepository guildRepository)
        {
            _guildRepository = guildRepository;
        }

        public async Task<Loan> CreateLoan(
            Guid id,
            Guid userId,
            Guid guildId,
            Decimal amount,
            String description,
            Int32 loanPeriod = 14,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tariff = await _guildRepository.GetTariff(guildId, cancellationToken);
            return new Loan(id, userId, tariff?.Id, description?.Trim(), DateTime.UtcNow.AddDays(loanPeriod), amount, 0);
        }
    }
}
