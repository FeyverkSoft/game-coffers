using System;
using Coffers.Public.Domain.Loans;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Loans
{

    public class LoanDbContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Guild> Guilds { get; set; }

        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

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

                b.Property(g => g.TariffId);

                b.HasOne(l => l.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.TariffId);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable(nameof(User));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .IsRequired();
                b.Property(g => g.GuildId)
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
                    .HasMaxLength(1024);

                b.HasOne(_ => _.User)
                    .WithMany()
                    .HasForeignKey(_ => _.UserId)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });

        }
    }
}
