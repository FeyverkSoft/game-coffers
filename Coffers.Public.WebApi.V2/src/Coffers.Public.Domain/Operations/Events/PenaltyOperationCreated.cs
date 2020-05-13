using System;
using Core.Rabbita;

namespace Coffers.Public.Domain.Operations.Events
{
    public sealed class PenaltyOperationCreated : IEvent
    {
        public Guid OperationId { get; }
        public Guid PenaltyId { get; }
        public PenaltyOperationCreated(Guid operationId, Guid penaltyId) => 
            (OperationId, PenaltyId) 
            = (operationId, penaltyId);
    }
}
