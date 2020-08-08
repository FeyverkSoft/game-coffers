using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.UserRegistration.Events;
using Coffers.Public.Infrastructure.EmailSender;

using Microsoft.Extensions.Logging;

using Rabbita.Core;

namespace Coffers.Public.WebApi.EventHandlers
{
    public sealed class ConfirmationCodeCreatedEventHandler : IEventHandler<ConfirmationCodeCreated>
    {
        private readonly ILogger _logger;
        private readonly EmailSender _emailSender;

        public ConfirmationCodeCreatedEventHandler(ILogger<ConfirmationCodeCreatedEventHandler> logger,
            EmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task Handle(
            ConfirmationCodeCreated message,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ConfirmationCodeCreated for email {message.Email} has received");

            await _emailSender.Send(new Email(
                to: message.Email,
                body: $"Добрый день!<br/> Для завершения регистрации в сервисе guild-treasury пожалуйста перейтиде по ссылке <br/> <a href=\"https://guild-treasury.ru/auth?code={message.ConfirmationCode}\">Подтвердить емайл!</a><br/><br/> ----<br/> С уважением администрация сервиса guild-treasury",
                subject: "Регистрация пользователя"
                ), cancellationToken);
            
            _logger.LogInformation($"ConfirmationCodeCreated for email {message.Email} has send");
        }
    }
}