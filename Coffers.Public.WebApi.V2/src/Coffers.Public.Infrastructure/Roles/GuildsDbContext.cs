using System;
using Coffers.Public.Domain.Roles;
using Coffers.Public.Domain.Roles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Roles
{
    public class GuildsDbContext : DbContext
    {
        public DbSet<Guild> Guilds { get; set; }

        public GuildsDbContext(DbContextOptions<GuildsDbContext> options) : base(options) { }

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

                b.HasMany(g => g.Roles)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.GuildId);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Tariff>(b =>
            {
                b.ToTable(nameof(Tariff));

                b.HasIndex(t => t.Id)
                    .IsUnique();
                b.HasKey(t => t.Id);
                b.Property(t => t.Id)
                    .ValueGeneratedNever()
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

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ToTable(nameof(UserRole));

                b.HasIndex(gt => new { gt.UserRoleId, gt.GuildId })
                    .IsUnique();
                b.HasKey(gt => new { gt.UserRoleId, gt.GuildId });

                b.Property(gt => gt.UserRoleId)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.GuildId)
                    .IsRequired();

                b.Property(t => t.TariffId)
                    .IsRequired();

                b.HasOne(_ => _.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.TariffId);
            });
        }
    }
}
