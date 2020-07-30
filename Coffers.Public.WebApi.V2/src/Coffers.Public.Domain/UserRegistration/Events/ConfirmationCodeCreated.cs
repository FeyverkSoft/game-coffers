using System;

using Rabbita.Core;

namespace Coffers.Public.Domain.UserRegistration.Events
{
    public sealed class ConfirmationCodeCreated : IEvent
    {
        public String ConfirmationCode { get; private set; }
        public String Email { get; private set; }

        protected ConfirmationCodeCreated() { }
        public ConfirmationCodeCreated(String confirmationCode, String email)
        => (ConfirmationCode, Email)
            = (confirmationCode, email);
    }
}
