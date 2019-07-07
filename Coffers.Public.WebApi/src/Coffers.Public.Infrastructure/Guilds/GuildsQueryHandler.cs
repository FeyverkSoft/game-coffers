using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Infrastructure.Helpers;
using Coffers.Public.Queries.Guilds;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Guilds
{
    public class GuildsQueryHandler : IQueryHandler<GuildQuery, GuildView>,
        IQueryHandler<GuildsQuery, ICollection<GuildView>>
    {
        private readonly GuildsDbContext _context;

        public GuildsQueryHandler(GuildsDbContext context)
        {
            _context = context;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            return await _context.Guilds
                .Where(guild => guild.Id == query.Id && guild.Gamers.Any(g => g.Id == query.UserId))
                .Include(x => x.Tariff)
                .Select(res => new GuildView
                {
                    Id = res.Id,
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
                    }
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
    }
}
