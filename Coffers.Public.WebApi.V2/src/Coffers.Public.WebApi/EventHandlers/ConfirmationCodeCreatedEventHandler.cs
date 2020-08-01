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
                body: $"Ваш код для подтверждания регистрации {message.ConfirmationCode}",
                subject: "Регистрация пользователя"
                ), cancellationToken);
            
            _logger.LogInformation($"ConfirmationCodeCreated for email {message.Email} has send");
        }
    }
}