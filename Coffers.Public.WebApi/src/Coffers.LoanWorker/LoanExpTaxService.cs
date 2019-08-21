using System;
using System.Threading;
using System.Threading.Tasks;
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
        private const Int32 SleepTime= 60*60*1000;
        private readonly ILogger _logger;

        public LoanExpTaxService(ILogger logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
