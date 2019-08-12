using System;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Queries.Infrastructure.Operations
{
    public class OperationsQueriesDbContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public OperationsQueriesDbContext(DbContextOptions<OperationsQueriesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

                b.HasOne(g => g.FromAccount)
                    .WithMany(_=>_.FromOperations)
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.ToAccount)
                    .WithMany(_ => _.ToOperations)
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

                b.HasMany(g => g.FromOperations)
                    .WithOne(_=>_.FromAccount)
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.ToOperations)
                    .WithOne(_=>_.ToAccount)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(a => a.ConcurrencyTokens)
                    .IsConcurrencyToken()
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

                b.HasOne(_ => _.Account)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();
            });

           
        }
    }
}
