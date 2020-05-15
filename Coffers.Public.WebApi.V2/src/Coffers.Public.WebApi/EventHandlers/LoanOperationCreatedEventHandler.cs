using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Events;
using Core.Rabbita;

namespace Coffers.Public.WebApi.EventHandlers
{
    public sealed class LoanOperationCreatedEventHandler : IEventHandler<LoanOperationCreated>
    {
        public Task Handle(LoanOperationCreated message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
