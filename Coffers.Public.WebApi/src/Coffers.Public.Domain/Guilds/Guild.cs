using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Coffers.Types.Gamer;
using Coffers.Types.Guilds;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Счёт гильдии
        /// </summary>
        public Account GuildAccount { get; set; }

        /// <summary>
        /// Название гильдии
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// тариф гильдии, действующий на данный момент
        /// </summary>
        public GuildTariff Tariff { get; set; }

        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; set; }

        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; set; }

        /// <summary>
        /// Список игроков в гильдии
        /// </summary>
        public ICollection<Gamer> Gamers { get; set; }

        internal Guild() { }

        public Guild(Guid id, String name, GuildStatus status, RecruitmentStatus recruitmentStatus)
        {
            Id = id;
            Name = name;
            RecruitmentStatus = recruitmentStatus;
            Status = status;
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            GuildAccount = new Account();
            Tariff = new GuildTariff();
        }
        public void AddGamer(String login)
        {
            foreach (var gamer in Gamers)
            {
                if (gamer.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase))
                {
                    gamer.Status = GamerStatus.Active;
                    gamer.Rank = GamerRank.Beginner;
                }
            }
            UpdateDate = DateTime.UtcNow;
        }
        public void AddGamer(Guid id, String name, String login, DateTime dateOfBirth, GamerStatus gamerStatus, GamerRank rank)
        {
            if (Gamers.Any(g => g.Id == id && !g.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)))
                throw new DuplicateNameException("Gamer already exists");
            Gamers.Add(new Gamer(id, name, login, dateOfBirth, gamerStatus, rank));
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateBeginnerTariff(Decimal beginnerTariffLoanTax,
            Decimal beginnerTariffExpiredLoanTax, Decimal[] beginnerTariffTax)
        {
            Tariff = new GuildTariff
            {
                Id = Guid.NewGuid(),
                BeginnerTariff = new Tariff(beginnerTariffLoanTax, beginnerTariffExpiredLoanTax, beginnerTariffTax),
                OfficerTariff = Tariff?.OfficerTariff,
                LeaderTariff = Tariff?.LeaderTariff,
                VeteranTariff = Tariff?.VeteranTariff,
                SoldierTariff = Tariff?.SoldierTariff
            };
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateOfficerTariff(Decimal officerTariffLoanTax,
            Decimal officerTariffExpiredLoanTax, Decimal[] officerTariffTax)
        {
            Tariff = new GuildTariff
            {
                Id = Guid.NewGuid(),
                BeginnerTariff = Tariff?.BeginnerTariff,
                OfficerTariff = new Tariff(officerTariffLoanTax, officerTariffExpiredLoanTax, officerTariffTax),
                LeaderTariff = Tariff?.LeaderTariff,
                VeteranTariff = Tariff?.VeteranTariff,
                SoldierTariff = Tariff?.SoldierTariff
            };
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateLeaderTariff(Decimal leaderTariffLoanTax,
            Decimal leaderTariffExpiredLoanTax, Decimal[] leaderTariffTax)
        {
            Tariff = new GuildTariff
            {
                Id = Guid.NewGuid(),
                BeginnerTariff = Tariff?.BeginnerTariff,
                OfficerTariff = Tariff?.OfficerTariff,
                LeaderTariff = new Tariff(leaderTariffLoanTax, leaderTariffExpiredLoanTax, leaderTariffTax),
                VeteranTariff = Tariff?.VeteranTariff,
                SoldierTariff = Tariff?.SoldierTariff
            };
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateVeteranTariff(Decimal veteranTariffLoanTax,
            Decimal veteranTariffExpiredLoanTax, Decimal[] veteranTariffTax)
        {
            Tariff = new GuildTariff
            {
                Id = Guid.NewGuid(),
                BeginnerTariff = Tariff?.BeginnerTariff,
                OfficerTariff = Tariff?.OfficerTariff,
                LeaderTariff = Tariff?.LeaderTariff,
                VeteranTariff = new Tariff(veteranTariffLoanTax, veteranTariffExpiredLoanTax, veteranTariffTax),
                SoldierTariff = Tariff?.SoldierTariff
            };
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateSoldierTariff(Decimal soldierTariffLoanTax,
            Decimal soldierTariffExpiredLoanTax, Decimal[] soldierTariffTax)
        {
            Tariff = new GuildTariff
            {
                Id = Guid.NewGuid(),
                BeginnerTariff = Tariff?.BeginnerTariff,
                OfficerTariff = Tariff?.OfficerTariff,
                LeaderTariff = Tariff?.LeaderTariff,
                VeteranTariff = Tariff?.VeteranTariff,
                SoldierTariff = new Tariff(soldierTariffLoanTax, soldierTariffExpiredLoanTax, soldierTariffTax)
            };
            UpdateDate = DateTime.UtcNow;
        }
    }
}
