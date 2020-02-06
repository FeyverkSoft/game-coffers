using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanExpireProcessor
    {
        private readonly IOperationRepository _operationRepository;
        public LoanExpireProcessor(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task Process(
            Loan loan,
            CancellationToken cancellationToken)
        {
            if (loan.LoanStatus == LoanStatus.Paid ||
                loan.LoanStatus == LoanStatus.Canceled ||
                loan.LoanStatus == LoanStatus.Expired)
                return;

            var operations = await _operationRepository.Get(loan.Id, cancellationToken);
            var loanAmount = loan.Amount + loan.TaxAmount + loan.PenaltyAmount;

            if (operations.Sum(_ => _.Amount) >= loanAmount)
                return;

            if (loan.ExpiredDate <= DateTime.UtcNow)
                loan.MakeExpired();
        }
    }
}
