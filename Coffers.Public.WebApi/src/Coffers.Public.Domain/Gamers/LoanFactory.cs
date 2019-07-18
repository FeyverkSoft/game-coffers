using System;
using System.Threading;
using Coffers.Public.Domain.Guilds;

namespace Coffers.Public.Domain.Gamers
{
    public sealed class LoanFactory
    {
        private readonly IGuildRepository _guildRepository;
        public Loan Build(Guid id, Guid guildId, Decimal amount,
            String description, DateTime borrowDate, DateTime expiredDate)
        {
            _guildRepository.Get(guildId, CancellationToken.None, true);
            throw new NotImplementedException();
        }
    }
}
