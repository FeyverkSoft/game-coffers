using System;

namespace Coffers.Public.Infrastructure.EmailSender
{
    public sealed class EmailSenderOptions
    {
        public String SmtpHost { get; set; }

        public Int32 SmtpPort { get; set; }

        public Boolean SmtpUseSsl { get; set; }

        public String SmtpLogin { get; set; }

        public String SmtpPassword { get; set; }

        public String From { get; set; }
    }
}
