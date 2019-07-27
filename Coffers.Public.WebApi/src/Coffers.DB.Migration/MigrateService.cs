using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.DB.Migrations
{
    public class MigrateService<TContext> : BackgroundService
        where TContext : DbContext
    {
        private readonly Int32 _retryCount;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public MigrateService(IServiceScopeFactory scopeFactory, ILogger<MigrateService<TContext>> logger)
        {
            _retryCount = 10;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting migrations...");
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                var attempt = 0;
                do
                {
                    if (stoppingToken.IsCancellationRequested)
                        return;

                    attempt++;
                    try
                    {

                        await context.Database.MigrateAsync(cancellationToken: stoppingToken);

                        _logger.LogInformation("Migrations ended...");
                        return;
                    }
                    catch (SocketException e)
                    {
                        _logger.LogError(e, $"Try #{attempt}; Connection to Database server FAILED");
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, $"Try #{attempt};");
                    }

                    await Task.Delay(attempt * 1000, stoppingToken);
                }
                while (attempt < _retryCount);
            }

        }
    }

}
