using System;
using Coffers.Public.Domain.Users.Entity;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Users
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

                b.Property(g => g.Name)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(g => g.DateOfBirth)
                    .IsRequired();
                b.Property(g => g.UpdateDate)
                    .IsRequired();
                b.Property(g => g.DeletedDate)
                    .IsRequired(false);

                b.Property(o => o.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();

                b.HasMany(_ => _.Characters)
                    .WithOne()
                    .HasForeignKey(_ => _.UserId)
                    .HasPrincipalKey(_ => _.Id);
            });

            modelBuilder.Entity<Character>(b =>
            {
                b.ToTable(nameof(Character));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.ClassName)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(g => g.Name)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(t => t.IsMain)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasMaxLength(32)
                    .HasConversion<String>();
            });
        }
    }
}
