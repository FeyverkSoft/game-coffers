using System;
using Coffers.DB.Migrations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffers.DB.Migrations
{
    public class MigrateDbContext
        : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options) : base(options)
        {
        }

        /* public DbSet<Guild> Guilds { get; set; }
         public DbSet<Tariff> Tariffs { get; set; }
         public DbSet<GuildTariff> GuildTariffs { get; set; }
         public DbSet<Gamer> Gamers { get; set; }
         public DbSet<Loan> Loans { get; set; }
         public DbSet<Penalty> Penalties { get; set; }
         public DbSet<History> Histories { get; set; }
         public DbSet<Account> Accounts { get; set; }
         public DbSet<Operation> Operations { get; set; }
         public DbSet<Character> Characters { get; set; }
         */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
                    .WithOne(_ => _.Guild)
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.GuildAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
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
                    .HasDefaultValue("{}")
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

                b.Property(t => t.CreateDate)
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

            modelBuilder.Entity<Gamer>(b =>
            {
                b.ToTable(nameof(Gamer));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.UpdateDate);
                b.Property(g => g.DeletedDate)
                    .IsRequired(false);

                b.Property(g => g.DateOfBirth)
                    .HasDefaultValue(new DateTime(1900, 1, 1))
                    .IsRequired();

                b.Property(g => g.Name)
                    .HasMaxLength(64);
                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);
                b.Property(g => g.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(g => g.Login)
                    .IsRequired()
                    .HasMaxLength(64);
                b.Property(g => g.Password)
                    .HasMaxLength(128);
                b.Property(g => g.Roles)
                    .HasMaxLength(512);


                b.HasMany(g => g.Characters)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Loans)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Penalties)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.DefaultAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();

                b.HasMany(g => g.Histories)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<Character>(b =>
            {
                b.ToTable(nameof(Character));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.ClassName)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(g => g.Name)
                    .HasMaxLength(64)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasMaxLength(32)
                    .HasConversion<String>();
            });

            modelBuilder.Entity<Loan>(b =>
            {
                b.ToTable(nameof(Loan));

                b.HasIndex(l => l.Id)
                    .IsUnique();
                b.HasKey(l => l.Id);
                b.Property(l => l.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(l => l.CreateDate)
                    .IsRequired();
                b.Property(l => l.BorrowDate)
                    .IsRequired();
                b.Property(l => l.UpdateDate);
                b.Property(l => l.ExpiredDate)
                    .IsRequired();
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.LoanStatus)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();
                b.Property(l => l.PenaltyAmount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.TaxAmount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.Description)
                    .HasMaxLength(512);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

                b.HasOne(g => g.Account)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();

                b.HasOne(l => l.Gamer)
                    .WithMany(_ => _.Loans)
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(l => l.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
            });

            modelBuilder.Entity<Penalty>(b =>
            {
                b.ToTable(nameof(Penalty));

                b.HasIndex(p => p.Id)
                    .IsUnique();
                b.HasKey(p => p.Id);
                b.Property(p => p.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(p => p.CreateDate)
                    .IsRequired();

                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();

                b.Property(p => p.Description)
                    .HasMaxLength(2048);

                b.Property(p => p.PenaltyStatus)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasOne(p => p.Gamer)
                    .WithMany(_ => _.Penalties)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

            });

            modelBuilder.Entity<History>(b =>
            {
                b.ToTable(nameof(History));

                b.HasIndex(h => h.Id)
                    .IsUnique();
                b.HasKey(h => h.Id);
                b.Property(h => h.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(h => h.CreateDate)
                    .IsRequired();
                b.Property(h => h.Action)
                    .HasMaxLength(1024)
                    .IsRequired();

                b.HasOne(p => p.Gamer)
                    .WithMany(_ => _.Histories)
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

                b.Property(a => a.Balance)
                    .HasDefaultValue(0)
                    .IsRequired();

                b.Property(a => a.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();

                b.HasMany(a => a.FromOperations)
                    .WithOne(_ => _.FromAccount)
                    .HasPrincipalKey(_ => _.Id);
                b.HasMany(a => a.ToOperations)
                    .WithOne(_ => _.ToAccount)
                    .HasPrincipalKey(_ => _.Id);
            });

            modelBuilder.Entity<Operation>(b =>
            {
                b.ToTable(nameof(Operation));

                b.HasIndex(o => o.Id)
                    .IsUnique();
                b.HasKey(o => o.Id);
                b.Property(o => o.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(o => o.CreateDate)
                    .IsRequired();
                b.Property(o => o.OperationDate)
                    .IsRequired();
                b.Property(o => o.DocumentId);
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(o => o.Description)
                    .HasMaxLength(512);
                b.Property(o => o.Type)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasOne(g => g.FromAccount)
                    .WithMany(_ => _.FromOperations)
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.ToAccount)
                    .WithMany(_ => _.ToOperations)
                    .HasPrincipalKey(_ => _.Id);
            });

            modelBuilder.Entity<Session>(b =>
            {
                b.ToTable(nameof(Session));

                b.HasIndex(o => o.SessionId)
                    .IsUnique();
                b.HasKey(o => o.SessionId);
                b.Property(o => o.SessionId)
                    .HasColumnName("SessionId")
                    .IsRequired();

                b.Property(o => o.CreateDate)

                    .IsRequired();
                b.Property(o => o.ExpireDate)
                    .IsRequired();

                b.HasOne(g => g.Gamer)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

                b.Property(o => o.Ip)
                    .HasMaxLength(128);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}