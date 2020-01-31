using System;
using Coffers.Public.Domain.Penalties;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Penalties
{

    public class PenaltyDbContext : DbContext
    {
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public PenaltyDbContext(DbContextOptions<PenaltyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Penalty>(b =>
            {
                b.ToTable(nameof(Penalty));

                b.HasIndex(p => p.Id)
                    .IsUnique();
                b.HasKey(p => p.Id);
                b.Property(p => p.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(p => p.CreateDate)
                    .IsRequired();
                b.Property(p => p.UpdateDate)
                    .IsRequired();

                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();

                b.Property(p => p.Description)
                    .HasMaxLength(2048);

                b.Property(p => p.PenaltyStatus)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasOne(p => p.User)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

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

                b.Property(g => g.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasMany(g => g.Penalties)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);
                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
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
