using System;
using Coffers.Public.Domain.Operations;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Operations
{

    public class OperationDbContext : DbContext
    {
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
                b.Property(o => o.DocumentId)
                .IsRequired(false);
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(o => o.Type)
                    .HasConversion<String>()
                    .HasMaxLength(32);
            });
        }
    }
}
