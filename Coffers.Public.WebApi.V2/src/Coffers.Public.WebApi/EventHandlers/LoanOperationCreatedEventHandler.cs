using System;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.Loans;
using Coffers.Public.Domain.Operations.Events;

using Microsoft.Extensions.Logging;

using Rabbita.Core;

namespace Coffers.Public.WebApi.EventHandlers
{
    public sealed class LoanOperationCreatedEventHandler : IEventHandler<LoanOperationCreated>
    {
        private readonly LoanProcessor _loanProcessor;
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger _logger;

        public LoanOperationCreatedEventHandler(
            LoanProcessor loanProcessor,
            ILoanRepository loanRepository,
            ILogger<LoanOperationCreatedEventHandler> logger
            )
        {
            _loanProcessor = loanProcessor;
            _loanRepository = loanRepository;
            _logger = logger;
        }

        public async Task Handle(
            LoanOperationCreated message,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoanOperationCreated for loan {message.LoanId} has received");
            var loan = await _loanRepository.Get(message.LoanId, cancellationToken);
            if (loan == null)
                throw new InvalidOperationException($"Loan: {message.LoanId} not found");
            await _loanProcessor.Process(loan, cancellationToken);
            await _loanRepository.Save(loan);
        }
    }
}