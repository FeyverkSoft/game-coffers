using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.LoanWorker.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.LoanWorker
{
    /// <summary>
    /// Сервис производящий начисление пени на один день просрочки по займу
    /// </summary>
    public sealed class LoanExpTaxService : BackgroundService
    {
        /// <summary>
        /// Время сна между повторным выполнением задачи
        /// пока что захардкодил один час.
        /// По идее задача должна выполняться раз в день.
        /// Но на случай форс-мажаорных ситуаций будем выполнять раз в час.
        /// Тем более сервис идемпотентен
        /// </summary>
        private const Int32 SleepTime = 60 * 60 * 1000;
        private readonly ILogger _logger;

        private readonly ILoanRepository _loanRepository;
        private readonly LoanTaxService _loanTaxService;

        public LoanExpTaxService(
                ILogger logger,
                ILoanRepository loanRepository,
                LoanTaxService loanTaxService
            )
        {
            _logger = logger;
            _loanRepository = loanRepository;
            _loanTaxService = loanTaxService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting loan service...");
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var loan in await _loanRepository.GetExpiredLoans(stoppingToken))
                {
                    loan.Expire();
                    await _loanTaxService.ProcessExpireLoan(loan);
                    await _loanRepository.SaveLoan(loan);
                }
                await Task.Delay(SleepTime, stoppingToken);
            }
            _logger.LogInformation("Stop loan service...");
            return;
        }
    }
}
