using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Rabbita.FluentExtensions;
using Core.Rabbita.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Rabbita.InProc
{
    internal sealed class InternalEventProcessor : BackgroundService
    {
        private readonly AsyncConcurrentQueue<IEvent> _queue;
        private readonly IEventHandlerRegistry _dispatchers;
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;

        public InternalEventProcessor(
            AsyncConcurrentQueue<IEvent> queue,
            IServiceProvider provider,
            IEventHandlerRegistry dispatchers,
            ILogger<InternalEventProcessor> logger)
        {
            _queue = queue;
            _dispatchers = dispatchers;
            _provider = provider;
            _logger = logger;
        }

        internal async void ListenMessages(CancellationToken cancellationToken)
        {
            await Task.Yield();
            using var scope = _provider.CreateScope();
            while (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    if (_queue.Count > 0)
                    {
                        var message = await _queue.DequeueAsync(cancellationToken);
                        var processor = scope.ServiceProvider.GetService(_dispatchers.GetHandlerFor(message));
                        var method = processor.GetType().GetMethod(nameof(IEventHandler<IEvent>.Handle));
                        try
                        {
                            await (Task)method.Invoke(processor, new Object[] { message, cancellationToken });
                        }
                        catch (Exception e)
                        {
                            _logger.LogError($"{processor}: {e.Message}", e);
                        }
                    }
                }
                catch (InvalidOperationException e) { _logger.LogError(e.Message, e); }

                await Task.Delay(1, cancellationToken);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ListenMessages(stoppingToken);
            return Task.CompletedTask;
        }
    }
}
