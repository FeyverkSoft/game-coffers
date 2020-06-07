using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans.Entity;

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
            // не суммируем Amount так как операция взятия займа даёт -Amount а операция выплаты даст нам Amount в итоге 0
            var loanAmount = loan.TaxAmount + loan.PenaltyAmount;

            if (operations.Sum(_ => _.Amount) >= loanAmount)
                loan.MakePaid();
        }
    }
}
