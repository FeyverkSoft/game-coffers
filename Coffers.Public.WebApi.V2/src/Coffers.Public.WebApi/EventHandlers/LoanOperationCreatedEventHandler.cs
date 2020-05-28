using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Events;
using Rabbita.Core;

namespace Coffers.Public.WebApi.EventHandlers
{
    public sealed class LoanOperationCreatedEventHandler : IEventHandler<LoanOperationCreated>
    {
        public Task Handle(LoanOperationCreated message, CancellationToken cancellationToken)
        {
            return Task.Delay(50000, cancellationToken);
        }
    }
}