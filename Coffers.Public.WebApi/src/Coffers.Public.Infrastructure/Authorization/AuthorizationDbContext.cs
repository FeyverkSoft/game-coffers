using System;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Authorization
{

    public class AuthorizationDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Gamer> Gamers { get; set; }

        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Session>(b =>
            {
                b.ToTable(nameof(Session));

                b.HasIndex(o => o.SessionId)
                    .IsUnique();
                b.HasKey(o => o.SessionId);
                b.Property(o => o.SessionId)
                    .HasColumnName("SessionId")
                    .IsRequired();

                b.Property(o => o.CreateDate)

                    .IsRequired();
                b.Property(o => o.ExpireDate)
                    .IsRequired();

                b.Property(o => o.Ip)
                    .HasMaxLength(128);

                b.HasOne(g => g.Gamer)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<Gamer>(b =>
            {
                b.ToTable(nameof(Gamer));

                b.HasIndex(o => o.Id)
                    .IsUnique();
                b.HasKey(o => o.Id);
                b.Property(o => o.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(o => o.Login)
                    .HasColumnName("Login")
                    .IsRequired();
                b.Property(o => o.Password)
                    .HasColumnName("Password");
                b.Property(o => o.GuildId)
                    .HasColumnName("GuildId")
                    .IsRequired();

                b.Property(o => o.Roles)
                    .HasConversion(
                        converterTo => converterTo == null ? null : converterTo.ToJson(),
                        converterForm => converterForm == null ? new String[] { } : converterForm.ParseJson<String[]>()
                        )
                    .HasMaxLength(512);
                b.Property(o => o.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

            });

        }
    }
}
