using System;

using Coffers.Public.Domain.UserRegistration;

using Microsoft.EntityFrameworkCore;

using Rabbita.Entity;
using Rabbita.Entity.FluentExtensions;

namespace Coffers.Public.Infrastructure.UserRegistration
{
    public class UserDbContext : PersistentMessagingDbContext
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
                    .IsRequired()
                    .HasMaxLength(64);

                b.Property(o => o.GuildId)
                    .IsRequired();

                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(o => o.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Property(g => g.Name)
                    .HasMaxLength(64);
                b.Property(g => g.Email)
                    .HasMaxLength(256)
                    .IsRequired(false);

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.DateOfBirth)
                    .IsRequired(false);

                b.Property(g => g.UpdateDate)
                    .IsRequired();

                b.Property(o => o.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();

                b.IsEvents(_ => _.Events);
            });

        }
    }
}
