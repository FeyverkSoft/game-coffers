﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Queries.Guilds;
using Coffers.Types.Account;
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
            var skipGmSta = new[] { GamerStatus.Left, GamerStatus.Banned };
            var q = _context.Guilds
               .AsNoTracking()
               .Where(guild => guild.Id == query.Id)
               .Include(x => x.Tariff)
               .Include(g => g.Gamers)
               .ThenInclude(c => c.Characters);

            return await q.Select(res => new GuildView
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
                GamersCount = res.Gamers.Count(g => !skipGmSta.Contains(g.Status)),
                CharactersCount = res.Gamers.Where(g => !skipGmSta.Contains(g.Status))
                         .SelectMany(x => x.Characters).Count(c => c.Status == CharStatus.Active),
                Balance = res.GuildAccount.Balance

            })
            .FirstOrDefaultAsync(cancellationToken);
        }

        private static TariffView TariffViewBuilder(Tariff tariff)
        {
            return new TariffView
            {
                Tax = tariff?.Tax ?? new[] { 0m }.ToList(),
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
                    Name = res.Name,
                    RecruitmentStatus = res.RecruitmentStatus,
                    Status = res.Status,
                    UpdateDate = res.UpdateDate,
                    CreateDate = res.CreateDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<GuildBalanceView> Handle(GuildBalanceQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Guilds
                .AsNoTracking()
                .Where(guild => guild.Id == query.GuildId)
                .Include(g => g.GuildAccount)
                .ThenInclude(ga => ga.Operations)
                .Include(g => g.Gamers)
                .ThenInclude(gm => gm.Characters)
                .Include(g => g.Gamers)
                .ThenInclude(gm => gm.Loans);

            var skipLoanStat = new[] { LoanStatus.Paid, LoanStatus.Canceled };
            var skipChStat = new[] { CharStatus.Deleted, CharStatus.Left };

            var temp = await q.Select(g => new
            {
                Balance = g.GuildAccount.Balance,
                ActiveLoansAmount = g.Gamers.Sum(gm => gm.Loans
                    .Where(l => !skipLoanStat.Contains(l.LoanStatus))
                    .Sum(l => l.Amount - l.RepaymentAmount)),
                ExpectedTaxAmount = g.Gamers
                    .Select(gm => new
                    {
                        Rank = gm.Rank,
                        Characters = gm.Characters.Count(c => !skipChStat.Contains(c.Status)),
                    }).ToList(),
                TaxAmount = g.GuildAccount.Operations.Where(o => o.Type == OperationType.Tax && o.OperationDate >= DateTime.UtcNow.Trunc(DateTruncType.Month))
                    .Sum(o => o.Amount)
            })
            .FirstOrDefaultAsync(cancellationToken);

            //далее костыль, так как из-за бага EF посчитать сразу сумму ожидаемого дохода не можем
            var tariff = _context.Guilds
                .AsNoTracking()
                .Where(guild => guild.Id == query.GuildId)
                .Include(g => g.Tariff)
                .ThenInclude(gt => gt.BeginnerTariff)
                .Include(g => g.Tariff)
                .ThenInclude(gt => gt.SoldierTariff)
                .Include(g => g.Tariff)
                .ThenInclude(gt => gt.VeteranTariff)
                .Include(g => g.Tariff)
                .ThenInclude(gt => gt.OfficerTariff)
                .Include(g => g.Tariff)
                .ThenInclude(gt => gt.LeaderTariff);

            return new GuildBalanceView
            {
                Balance = temp.Balance,
                ExpectedTaxAmount = temp.ExpectedTaxAmount.Sum(gt => CalcTax(tariff.FirstOrDefault()?.Tariff, gt.Rank, gt.Characters)),
                ActiveLoansAmount = temp.ActiveLoansAmount,
                TaxAmount = temp.TaxAmount
            };
        }

        private static Decimal CalcTax(GuildTariff tariff, GamerRank gmRank, Int32 count)
        {
            var index = count > 0 ? count - 1 : 0;
            IList<Decimal> tax = null;
            switch (gmRank)
            {
                case GamerRank.Leader:
                    tax = tariff.LeaderTariff?.Tax;
                    break;
                case GamerRank.Officer:
                    tax = tariff.OfficerTariff?.Tax;
                    break;
                case GamerRank.Veteran:
                    tax = tariff.VeteranTariff?.Tax;
                    break;
                case GamerRank.Soldier:
                    tax = tariff.SoldierTariff?.Tax;
                    break;
                case GamerRank.Beginner:
                    tax = tariff.BeginnerTariff?.Tax;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gmRank), gmRank, null);
            }

            if (tax == null)
                return 0;
            if (index > tax.Count - 1)
                return tax.Last() * count;
            return tax[index] * count;
        }
    }
}
