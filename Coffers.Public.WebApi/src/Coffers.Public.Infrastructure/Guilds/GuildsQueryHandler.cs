using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Infrastructure.Helpers;
using Coffers.Public.Queries.Guilds;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Guilds
{
    public class GuildsQueryHandler : IQueryHandler<GuildQuery, GuildView>,
        IQueryHandler<GuildsQuery, ICollection<GuildView>>,
        IQueryHandler<GuildBalanceQuery, GuildBalanceView>

    {
        private readonly GuildsDbContext _context;

        public GuildsQueryHandler(GuildsDbContext context)
        {
            _context = context;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            return await _context.Guilds
                .AsNoTracking()
                .Where(guild => guild.Id == query.Id)
                .Include(x => x.Tariff)
                .Include(g => g.Gamers)
                .ThenInclude(c => c.Characters)
                .Select(res => new GuildView
                {
                    Id = res.Id,
                    Name = res.Name,
                    RecruitmentStatus = res.RecruitmentStatus,
                    Status = res.Status,
                    UpdateDate = res.UpdateDate,
                    CreateDate = res.CreateDate,
                    Tariffs = new TariffsView
                    {
                        Beginner = TariffViewBuilder(res.Tariff != null ? res.Tariff.BeginnerTariff : null),
                        Officer = TariffViewBuilder(res.Tariff != null ? res.Tariff.OfficerTariff : null),
                        Leader = TariffViewBuilder(res.Tariff != null ? res.Tariff.LeaderTariff : null),
                        Veteran = TariffViewBuilder(res.Tariff != null ? res.Tariff.VeteranTariff : null),
                        Soldier = TariffViewBuilder(res.Tariff != null ? res.Tariff.SoldierTariff : null)
                    },
                    GamersCount = res.Gamers.Count(g => !new[] { GamerStatus.Left, GamerStatus.Banned }.Contains(g.Status)),
                    CharactersCount = res.Gamers.Where(g => !new[] { GamerStatus.Left, GamerStatus.Banned }.Contains(g.Status))
                        .SelectMany(x => x.Characters).Count(c => c.Status == CharStatus.Active),
                    Balance = res.GuildAccount.Balance

                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        private static TariffView TariffViewBuilder(Tariff tariff)
        {
            return new TariffView
            {
                Tax = tariff?.Tax?.ParseJson<Decimal[]>() ?? new[] { 0m },
                LoanTax = tariff?.LoanTax ?? 0m,
                ExpiredLoanTax = tariff?.ExpiredLoanTax ?? 0m
            };
        }
        public async Task<ICollection<GuildView>> Handle(GuildsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Guilds
                .AsNoTracking()
                .Select(res => new GuildView
                {
                    Id = res.Id,
                    RecruitmentStatus = res.RecruitmentStatus,
                    Status = res.Status,
                    UpdateDate = res.UpdateDate,
                    CreateDate = res.CreateDate
                })
                .ToListAsync(cancellationToken);
        }

        public Task<GuildBalanceView> Handle(GuildBalanceQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Guilds
                  .AsNoTracking()
                  .Where(guild => guild.Id == query.GuildId)
                  .Include(g => g.GuildAccount)
                  .Include(x => x.Tariff)
                  .Include(g => g.Gamers)
                  .ThenInclude(c => c.Characters)
                  .Include(g => g.Gamers)
                  .ThenInclude(c => c.Loans);

            throw new NotImplementedException();
        }
    }
}
