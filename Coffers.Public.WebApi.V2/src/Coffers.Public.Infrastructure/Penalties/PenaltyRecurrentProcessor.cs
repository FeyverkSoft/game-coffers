using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.Public.Infrastructure.Penalties
{
    public sealed class PenaltyRecurrentProcessor : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IPenaltyRepository _repository;
        private readonly PenaltyProcessor _penaltyProcessor;

        public PenaltyRecurrentProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger<PenaltyRecurrentProcessor> logger)
        {
            _logger = logger;
            _repository = scopeFactory.CreateScope().ServiceProvider.GetService<IPenaltyRepository>();
            _penaltyProcessor = scopeFactory.CreateScope().ServiceProvider.GetService<PenaltyProcessor>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _logger.LogInformation("PenaltyRecurrentProcessor stopped"));
            while (!cancellationToken.IsCancellationRequested){
                await PenaltyWorker(cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async Task PenaltyWorker(CancellationToken cancellationToken)
        {
            try{
                var penalties = await _repository.GetActivePenalties(cancellationToken);
                foreach (var penalty in penalties){
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    try{
                        _logger.LogInformation($"PenaltyWorker: Start process penalty: {penalty.Id}");
                        await _penaltyProcessor.Process(penalty, cancellationToken);
                        await _repository.Save(penalty);
                        _logger.LogInformation($"PenaltyWorker: End process penalty: {penalty.Id}");
                    }
                    catch (Exception e){
                        _logger.LogError(e, $"PenaltyWorker: {e.Message}");
                    }
                }
            }
            catch (Exception e){
                _logger.LogError(e, $"PenaltyWorker: {e.Message}");
            }
        }
    }
}