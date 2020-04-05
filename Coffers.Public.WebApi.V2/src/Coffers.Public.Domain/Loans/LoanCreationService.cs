using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanCreationService
    {
        private readonly IGuildRepository _guildRepository;
        private readonly ILoanRepository _loanRepository;
        public LoanCreationService(IGuildRepository guildRepository,
            ILoanRepository loanRepository)
        {
            _guildRepository = guildRepository;
            _loanRepository = loanRepository;
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
            if (!await _guildRepository.IsUserExists(userId, guildId, cancellationToken))
                throw new UserNotFoundException(userId);

            var tariff = await _guildRepository.GetTariff(userId, guildId, cancellationToken);

            var existsLoan = await _loanRepository.Get(id, cancellationToken);

            if (existsLoan != null)
                if (!existsLoan.Description.Equals(description.Trim()) ||
                    existsLoan.Amount != amount ||
                    existsLoan.UserId != userId)
                    throw new LoanAlreadyExistsException(existsLoan);
                else
                    return existsLoan;

            return new Loan(id, userId, tariff?.Id, description?.Trim(), DateTime.UtcNow.AddDays(loanPeriod), amount, 0);

        }
    }
}
