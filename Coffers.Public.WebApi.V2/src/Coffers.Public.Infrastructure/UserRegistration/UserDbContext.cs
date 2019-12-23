using System;
using Coffers.Public.Domain.UserRegistration;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.UserRegistration
{

    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable(nameof(User));

                b.HasIndex(o => o.Id)
                    .IsUnique();
                b.HasKey(o => o.Id);
                b.Property(o => o.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();

                b.Property(o => o.Login)
                    .HasColumnName("Login")
                    .IsRequired();

                b.Property(o => o.GuildId)
                    .HasColumnName("GuildId")
                    .IsRequired();

                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(o => o.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Property(g => g.CreateDate)
                    .IsRequired();

                b.Property(g => g.UpdateDate)
                    .IsRequired();

                b.Property(o => o.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
            });

        }
    }
}
