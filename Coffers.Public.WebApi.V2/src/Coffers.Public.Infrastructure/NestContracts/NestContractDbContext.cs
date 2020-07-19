using System;
using Coffers.Public.Domain.NestContracts;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.NestContracts
{
    public class NestContractDbContext : DbContext
    {
        public DbSet<NestContract> NestContracts { get; protected set; }
        public DbSet<Nest> Nests { get; protected set; }

        public NestContractDbContext(DbContextOptions<NestContractDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Nest>(b =>
            {
                b.ToTable(nameof(Nest));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.IsHidden);

                b.Property(n => n.GuildId)
                    .IsRequired();
            });

            modelBuilder.Entity<NestContract>(b =>
            {
                b.ToTable(nameof(NestContract));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.CharacterName)
                    .HasColumnName("Name")
                    .HasMaxLength(64)
                    .IsRequired();

                b.Property(n => n.UserId)
                    .IsRequired();

                b.Property(n => n.NestId)
                    .IsRequired();
                
                b.Property(n => n.Reward)
                    .HasMaxLength(512)
                    .IsRequired();
                b.Property(n => n.Status)
                    .HasConversion<String>()
                    .HasMaxLength(16)
                    .IsRequired();

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });
        }
    }
}