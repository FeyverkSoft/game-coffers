using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.OperDay.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coffers.OperDay
{
    /// <summary>
    /// Сервис производящий фиксирование статистики опер дней
    /// </summary>
    public sealed class OperDayService<TLoanContext> : BackgroundService
        where TLoanContext : OperDayDbContext
    {
        /// <summary>
        /// Время сна между повторным выполнением задачи
        /// пока что захардкодил один час.
        /// По идее задача должна выполняться раз в день.
        /// Но на случай форс-мажаорных ситуаций будем выполнять раз в 12 часов.
        /// Тем более сервис идемпотентен
        /// </summary>
        private const Int32 SleepTime = 12 * 60 * 60 * 1000;
        private readonly ILogger _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        public OperDayService(
            IServiceScopeFactory scopeFactory,
            ILogger<OperDayService<TLoanContext>> logger
            )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            var loanContext = scopeFactory.CreateScope().ServiceProvider.GetService<TLoanContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting OperDay service...");
            while (!stoppingToken.IsCancellationRequested)
            {
               
                await Task.Delay(SleepTime, stoppingToken);
            }
            _logger.LogInformation("Stop OperDay service...");
            return;
        }
    }
}
