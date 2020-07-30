using System;

namespace Coffers.Public.Infrastructure.EmailSender
{
    public sealed class Email
    {
        public String To { get; }
        
        public String Subject { get; }

        public String Body { get; }

        public Email(String to, String subject, String body)
        {
            this.To = to ?? throw new ArgumentNullException(nameof(to));
            this.Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            this.Body = body ?? throw new ArgumentNullException(nameof(body));
        }
    }
}
