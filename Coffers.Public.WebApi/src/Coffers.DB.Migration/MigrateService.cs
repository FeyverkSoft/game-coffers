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
    /// <summary>
    /// Обособленный Сервис миграции базы данных
    /// Создаёт таблицы, базу.
    /// Если необходимо накатывает последовательно миграции
    /// В будущем может быть вынесен в отдельный солюшен.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class MigrateService<TContext> : BackgroundService
        where TContext : DbContext
    {
        /// <summary>
        /// Колличество попыток миграции.
        /// По истечению которых мы считаем что миграция не прошла
        /// Необходимо из-за того что старт контейнера с базой не обзначает того что база та уже запустилась
        /// база в контейнере может долго подниматься
        /// </summary>
        private const Int32 RetryCount = 10;

        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public MigrateService(IServiceScopeFactory scopeFactory, ILogger<MigrateService<TContext>> logger)
        {
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
                while (attempt < RetryCount);
            }

        }
    }

}
