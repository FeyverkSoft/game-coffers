using System;
using Coffers.LoanWorker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coffers.LoanWorker.Infrastructure
{
    public class LoanWorkerDbContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public LoanWorkerDbContext(DbContextOptions<LoanWorkerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

                b.HasOne(_ => _.Account)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Operation>(b =>
            {
                b.ToTable(nameof(Operation));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.Type)
                    .HasConversion<String>();

                b.Property(g => g.DocumentId);

                b.Property(g => g.CreateDate)
                    .IsRequired();
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
            });

        }
    }
}
