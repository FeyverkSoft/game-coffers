using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    /// <summary>
    /// Фабрика порождающая объекты займов
    /// </summary>
    public sealed class LoanFactory
    {
        private readonly IGuildRepository _guildRepository;

        public LoanFactory(IGuildRepository guildRepository)
        {
            _guildRepository = guildRepository;
        }

        public async Task<Loan> Build(Guid id, Guid guildId, GamerRank rank, Decimal amount,
            String description, DateTime borrowDate, DateTime expiredDate)
        {
            var guild = await _guildRepository.Get(guildId, CancellationToken.None, true);
            Tariff t = null;
            switch (rank)
            {
                case GamerRank.Leader:
                    t = guild.Tariff.LeaderTariff;
                    break;
                case GamerRank.Officer:
                    t = guild.Tariff.OfficerTariff;
                    break;
                case GamerRank.Veteran:
                    t = guild.Tariff.VeteranTariff;
                    break;
                case GamerRank.Soldier:
                    t = guild.Tariff.SoldierTariff;
                    break;
                case GamerRank.Beginner:
                    t = guild.Tariff.BeginnerTariff;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rank), rank, null);
            }

            return new Loan(id, t.Id, amount, amount * (t.LoanTax / 100), description, borrowDate, expiredDate);
        }
    }
}
