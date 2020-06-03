using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Events;
using Coffers.Public.Domain.Penalties;
using Microsoft.Extensions.Logging;
using Rabbita.Core;

namespace Coffers.Public.WebApi.EventHandlers
{
    public sealed class PenaltyOperationCreatedEventHandler : IEventHandler<PenaltyOperationCreated>
    {
        private readonly PenaltyProcessor _penaltyProcessor;
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly ILogger _logger;

        public PenaltyOperationCreatedEventHandler(
            PenaltyProcessor penaltyProcessor,
            IPenaltyRepository penaltyRepository,
            ILogger<LoanOperationCreatedEventHandler> logger
        )
        {
            _penaltyProcessor = penaltyProcessor;
            _penaltyRepository = penaltyRepository;
            _logger = logger;
        }

        public async Task Handle(
            PenaltyOperationCreated message,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"PenaltyOperationCreated for penalty {message.PenaltyId} has received");
            var penalty = await _penaltyRepository.Get(message.PenaltyId, cancellationToken);
            if (penalty == null)
                throw new InvalidOperationException($"Penalty: {message.PenaltyId} not found");
            await _penaltyProcessor.Process(penalty, cancellationToken);
            await _penaltyRepository.Save(penalty);
        }
    }
}