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
        private readonly LoanExpireProcessor _loanExpireProcessor;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly LoanTaxProcessor _loanTaxProcessor;
        private readonly LoanProcessor _loanProcessor;

        public LoanRecurrentProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger<LoanRecurrentProcessor> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _loanExpireProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanExpireProcessor>();
            _loanTaxProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanTaxProcessor>();
            _loanProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<LoanProcessor>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _logger.LogInformation("LoanRecurrentProcessor stopped"));
            while (!cancellationToken.IsCancellationRequested){
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetService<ILoanRepository>();
                await LoanWorker(repository, cancellationToken);
                await LoanExpireWorker(repository, cancellationToken);
                await LoanExpireTaxWorker(repository, cancellationToken);
                await LoanActiveTaxWorker(repository, cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async Task LoanExpireWorker(ILoanRepository repository, CancellationToken cancellationToken)
        {
            try{
                var loans = await repository.GetAllUnprocessedExpiredLoan(cancellationToken);
                foreach (var loan in loans){
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try{
                        _logger.LogInformation($"ExpireWorker: Start process loan: {loan.Id}");
                        await _loanExpireProcessor.Process(loan, cancellationToken);
                        await repository.Save(loan, cancellationToken);
                        _logger.LogInformation($"ExpireWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e){
                        _logger.LogError(e, $"ExpireWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e){
                _logger.LogError(e, $"ExpireWorker: {e.Message}");
            }
        }

        private async Task LoanExpireTaxWorker(ILoanRepository repository, CancellationToken cancellationToken)
        {
            try{
                var loans = await repository.GetExpiredLoan(cancellationToken);
                foreach (var loan in loans){
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try{
                        _logger.LogInformation($"LoanExpireTaxWorker: Start process loan: {loan.Id}");
                        _loanTaxProcessor.ProcessExpireLoan(loan);
                        await _loanProcessor.Process(loan, cancellationToken);
                        await repository.Save(loan, cancellationToken);
                        _logger.LogInformation($"LoanExpireTaxWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e){
                        _logger.LogError(e, $"LoanExpireTaxWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e){
                _logger.LogError(e, $"LoanExpireTaxWorker: {e.Message}");
            }
        }

        private async Task LoanActiveTaxWorker(ILoanRepository repository, CancellationToken cancellationToken)
        {
            try{
                var loans = await repository.GetActiveLoan(cancellationToken);
                foreach (var loan in loans){
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try{
                        _logger.LogInformation($"LoanActiveTaxWorker: Start process loan: {loan.Id}");
                        _loanTaxProcessor.ProcessLoanTax(loan);
                        await repository.Save(loan, cancellationToken);
                        _logger.LogInformation($"LoanActiveTaxWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e){
                        _logger.LogError(e, $"LoanActiveTaxWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e){
                _logger.LogError(e, $"LoanActiveTaxWorker: {e.Message}");
            }
        }

        private async Task LoanWorker(ILoanRepository repository, CancellationToken cancellationToken)
        {
            try{
                var loans = await repository.GetActiveLoan(cancellationToken);
                foreach (var loan in loans){
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try{
                        _logger.LogInformation($"LoanWorker: Start process loan: {loan.Id}");
                        await _loanProcessor.Process(loan, cancellationToken);
                        await repository.Save(loan, cancellationToken);
                        _logger.LogInformation($"LoanWorker: End process loan: {loan.Id}");
                    }
                    catch (Exception e){
                        _logger.LogError(e, $"LoanWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e){
                _logger.LogError(e, $"LoanWorker: {e.Message}");
            }
        }
    }
}