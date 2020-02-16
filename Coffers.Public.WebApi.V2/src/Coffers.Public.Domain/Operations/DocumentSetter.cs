using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Entity;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public sealed class DocumentSetter
    {
        private readonly DocumentValidator _validator;

        public DocumentSetter(DocumentValidator validator)
        {
            _validator = validator;
        }

        public async Task<Operation> SetLoan(Operation operation, Guid loanId, CancellationToken cancellationToken)
        {
            if (await _validator.Validate(OperationType.Loan, loanId, operation.UserId, cancellationToken))
                operation.SetDocument(OperationType.Loan, loanId);
            return operation;
        }

        public async Task<Operation> SetPenalty(Operation operation, Guid penaltyId, CancellationToken cancellationToken)
        {
            if (await _validator.Validate(OperationType.Penalty, penaltyId, operation.UserId, cancellationToken))
                operation.SetDocument(OperationType.Penalty, penaltyId);
            return operation;
        }

        public async Task<Operation> SetTax(Operation operation, Guid taxId, CancellationToken cancellationToken)
        {
            if (await _validator.Validate(OperationType.Tax, taxId, operation.UserId, cancellationToken))
                operation.SetDocument(OperationType.Tax, taxId);
            return operation;
        }
    }
}
