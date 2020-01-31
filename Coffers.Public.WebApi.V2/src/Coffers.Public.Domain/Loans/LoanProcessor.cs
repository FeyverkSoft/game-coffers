using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanProcessor
    {
        private readonly IOperationRepository _operationRepository;
        public LoanProcessor(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task Process(
            Loan loan,
            CancellationToken cancellationToken)
        {
            if (!loan.IsActive)
                return;

            var operations = await _operationRepository.Get(loan.Id, cancellationToken);
            var loanAmount = loan.Amount + loan.TaxAmount + loan.PenaltyAmount;

            if (operations.Sum(_ => _.Amount) >= loanAmount)
                loan.MakePaid();

        }
    }
}
