using System;

namespace Coffers.Public.Infrastructure.UserRegistration
{
    public sealed class ConfirmationCodeProviderOptions
    {
        public String SecretKey { get; set; }
        public Int32? LifeTimeCodeInMinute { get; set; }
    }
}
