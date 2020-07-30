using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Coffers.Public.Infrastructure.EmailSender
{
    public sealed class EmailSender
    {
        private readonly EmailSenderOptions _options;
        private readonly ILoggerFactory _loggerFactory;

        public EmailSender(IOptions<EmailSenderOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _loggerFactory = loggerFactory;
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_options.SmtpHost, _options.SmtpPort)
            {
                EnableSsl = _options.SmtpUseSsl,
                Credentials = new NetworkCredential(_options.SmtpLogin, _options.SmtpPassword),
                UseDefaultCredentials = string.IsNullOrEmpty(_options.SmtpLogin) && string.IsNullOrEmpty(_options.SmtpPassword)
            };
        }

        public async Task Send(Email email, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<EmailSender>();
            try
            {
                using var smtpClient = CreateSmtpClient();
                var message = new MailMessage(_options.From, email.To, email.Subject, email.Body) {IsBodyHtml = true};
                await smtpClient.SendMailAsync(message);
                logger.LogInformation($"Email to {email.To} sent");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Email to {email.To} send is fail");
                throw;
            }
        }
    }
}
