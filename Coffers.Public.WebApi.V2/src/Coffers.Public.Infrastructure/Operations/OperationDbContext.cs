using System;
using Coffers.Public.Domain.Operations.Entity;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Operations
{

    public class OperationDbContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public OperationDbContext(DbContextOptions<OperationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                b.Property(o => o.DocumentId)
                    .IsRequired(false);
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(o => o.Description)
                    .HasMaxLength(1024);
                b.Property(o => o.Type)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(o => o.GuildId)
                    .IsRequired();
                b.Property(o => o.UserId)
                    .IsRequired();

                b.Property(o => o.ParentOperationId)
                    .IsRequired(false);

                b.Ignore(_ => _.Events);
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
                b.Property(l => l.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Tax>(b =>
            {
                b.ToTable(nameof(Tax));

                b.HasIndex(l => l.Id)
                    .IsUnique();
                b.HasKey(l => l.Id);
                b.Property(l => l.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(l => l.UserId)
                    .IsRequired();
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

                b.Property(l => l.UserId)
                    .IsRequired();
            });
        }
    }
}
