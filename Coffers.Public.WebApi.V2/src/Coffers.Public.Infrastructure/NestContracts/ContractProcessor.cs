using System;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.NestContracts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.Public.Infrastructure.NestContracts
{
    public sealed class ContractProcessor : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ContractProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger<ContractProcessor> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _logger.LogInformation("ContractProcessor stopped"));
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetService<INestContractRepository>();
                await ExpireWorker(repository, cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);
            }
        }

        private async Task ExpireWorker(INestContractRepository repository, CancellationToken cancellationToken)
        {
            try
            {
                var contracts = await repository.GetAllUnprocessedExpired(cancellationToken);
                foreach (var contract in contracts)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try
                    {
                        _logger.LogInformation($"ExpireWorker: Start process contract: {contract.Id}");
                        contract.MarkAsExpire();
                        await repository.Save(contract, cancellationToken);
                        _logger.LogInformation($"ExpireWorker: End process contract: {contract.Id}");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"ExpireWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"ExpireWorker: {e.Message}");
            }
        }
    }
}