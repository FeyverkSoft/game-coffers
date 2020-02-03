using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.Public.Infrastructure.Loans
{
    public sealed class LoanRecurrentProcessor : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ILoanRepository _repository;
        private readonly LoanExpireProcessor _loanExpireProcessor;
        private readonly LoanTaxProcessor _loanTaxProcessor;
        private readonly LoanProcessor _loanProcessor;
        public LoanRecurrentProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger logger)
        {
            _logger = logger;
            _repository = scopeFactory.CreateScope().ServiceProvider.GetService<ILoanRepository>();
            _loanExpireProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanExpireProcessor>();
            _loanTaxProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanTaxProcessor>();
            _loanProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanProcessor>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _logger.LogInformation("LoanRecurrentProcessor stopped"));
            while (!cancellationToken.IsCancellationRequested)
            {
                await LoanWorker(cancellationToken);
                await LoanExpireWorker(cancellationToken);
                await LoanExpireTaxWorker(cancellationToken);
                await LoanActiveTaxWorker(cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(2), cancellationToken);
            }
        }

        private async Task LoanExpireWorker(CancellationToken cancellationToken)
        {
            try
            {
                var loans = await _repository.GetAllUnprocessedExpiredLoan();
                foreach (var loan in loans)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try
                    {
                        _logger.LogInformation($"ExpireWorker: Start process loan: {loan.Id}");
                        await _loanExpireProcessor.Process(loan, cancellationToken);
                        await _repository.Save(loan);
                        _logger.LogInformation($"ExpireWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message, e);
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
        }

        private async Task LoanExpireTaxWorker(CancellationToken cancellationToken)
        {
            try
            {
                var loans = await _repository.GetExpiredLoan();
                foreach (var loan in loans)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try
                    {
                        _logger.LogInformation($"LoanExpireTaxWorker: Start process loan: {loan.Id}");
                        _loanTaxProcessor.ProcessExpireLoan(loan);
                        await _repository.Save(loan);
                        _logger.LogInformation($"LoanExpireTaxWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message, e);
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
        }

        private async Task LoanActiveTaxWorker(CancellationToken cancellationToken)
        {
            try
            {
                var loans = await _repository.GetActiveLoan();
                foreach (var loan in loans)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try
                    {
                        _logger.LogInformation($"LoanActiveTaxWorker: Start process loan: {loan.Id}");
                        _loanTaxProcessor.ProcessLoanTax(loan);
                        await _repository.Save(loan);
                        _logger.LogInformation($"LoanActiveTaxWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message, e);
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
        }

        private async Task LoanWorker(CancellationToken cancellationToken)
        {
            try
            {
                var loans = await _repository.GetActiveLoan();
                foreach (var loan in loans)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try
                    {
                        _logger.LogInformation($"LoanWorker: Start process loan: {loan.Id}");
                        await _loanProcessor.Process(loan, cancellationToken);
                        await _repository.Save(loan);
                        _logger.LogInformation($"LoanWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message, e);
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
        }
    }
}
