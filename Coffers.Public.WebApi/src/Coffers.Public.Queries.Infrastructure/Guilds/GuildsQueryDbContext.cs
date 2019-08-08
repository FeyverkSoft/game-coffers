using System;
using Coffers.Types.Gamer;
using Coffers.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public class GuildsQueryDbContext : DbContext
    {
        public DbSet<Guild> Guilds { get; set; }

        public GuildsQueryDbContext(DbContextOptions<GuildsQueryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Guild>(b =>
            {
                b.ToTable(nameof(Guild));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.UpdateDate);

                b.Property(g => g.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(512)
                    .IsRequired();

                b.Property(g => g.RecruitmentStatus)
                    .HasColumnName("RecruitmentStatus")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasColumnName("Status")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.HasOne(t => t.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Gamers)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.GuildAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<Gamer>(b =>
            {
                b.ToTable(nameof(Gamer));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);
                b.Property(g => g.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(g => g.Login)
                    .HasMaxLength(64);

                b.HasOne(g => g.DefaultAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Characters)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Loans)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);


            });
            modelBuilder.Entity<Account>(b =>
            {
                b.ToTable(nameof(Account));

                b.HasIndex(a => a.Id)
                    .IsUnique();
                b.HasKey(a => a.Id);
                b.Property(a => a.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.HasMany(g => g.ToOperations)
                    .WithOne(_=>_.ToAccount)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(a => a.Balance)
                    .HasDefaultValue(0)
                    .IsRequired();
            });

            modelBuilder.Entity<Tariff>(b =>
            {
                b.ToTable(nameof(Tariff));

                b.HasIndex(t => t.Id)
                    .IsUnique();
                b.HasKey(t => t.Id);
                b.Property(t => t.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.ExpiredLoanTax)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(t => t.LoanTax)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(t => t.Tax)
                    .HasMaxLength(4096)
                    .HasConversion(
                        _ => _ == null ? null : _.ToJson(),
                        _ => _ == null ? new Decimal[] { } : _.ParseJson<Decimal[]>()
                    )
                    .IsRequired();
            });

            modelBuilder.Entity<Loan>(b =>
            {
                b.ToTable(nameof(Loan));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.LoanStatus)
                    .HasConversion<String>()
                    .IsRequired();
            });

            modelBuilder.Entity<GuildTariff>(b =>
            {
                b.ToTable(nameof(GuildTariff));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.HasOne(t => t.BeginnerTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.LeaderTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.OfficerTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.SoldierTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.VeteranTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<Character>(b =>
            {
                b.ToTable(nameof(Character));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.Status)
                    .HasDefaultValue(CharStatus.Active)
                    .HasConversion<String>()
                    .IsRequired();
            });

            modelBuilder.Entity<Operation>(b =>
            {
                b.ToTable(nameof(Operation));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(o => o.Type)
                    .HasConversion<String>();

                b.Property(t => t.Type)
                    .HasConversion<String>()
                    .IsRequired();
            });

        }
    }
}
