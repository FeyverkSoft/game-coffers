using System;

using Rabbita.Core;

namespace Coffers.Public.Domain.UserRegistration.Events
{
    public sealed class SendConfirmationCode : IEvent
    {
        public String ConfirmationCode { get; private set; }
        public String Email { get; private set; }

        protected SendConfirmationCode() { }
        public SendConfirmationCode(String confirmationCode, String email)
        => (ConfirmationCode, Email)
            = (confirmationCode, email);
    }
}
