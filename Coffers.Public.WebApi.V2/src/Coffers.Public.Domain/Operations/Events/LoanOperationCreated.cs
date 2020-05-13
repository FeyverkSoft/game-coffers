using System;
using Core.Rabbita;

namespace Coffers.Public.Domain.Operations.Events
{
    public sealed class LoanOperationCreated : IEvent
    {
        public Guid OperationId { get; }
        public Guid LoanId { get; }
        public LoanOperationCreated(Guid operationId, Guid loanId) =>
            (OperationId, LoanId)
            = (operationId, loanId);
    }
}
