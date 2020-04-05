using System;
using Coffers.DB.Migrations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffers.DB.Migrations
{
    public class MigrateDbContext : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guild>(b =>
            {
                b.ToTable(nameof(Guild));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.UpdateDate);

                b.Property(g => g.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(512)
                    .IsRequired();

                b.Property(g => g.RecruitmentStatus)
                    .HasColumnName("RecruitmentStatus")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasColumnName("Status")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.HasMany(g => g.Users)
                    .WithOne(_ => _.Guild)
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.GuildId);

                b.HasMany(g => g.Roles)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.GuildId);

                b.HasMany(g => g.Operations)
                    .WithOne()
                    .HasForeignKey(_ => _.GuildId)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

                b.HasData(new Guild(
                    id: new Guid("00000000-0000-4000-0000-000000000001"),
                    recruitmentStatus: Types.Guilds.RecruitmentStatus.Close,
                    name: "Admins",
                    createDate: new DateTime(2020, 01, 30, 7, 35, 9, DateTimeKind.Utc),
                    updateDate: new DateTime(2020, 01, 30, 7, 35, 9, DateTimeKind.Utc),
                    status: Types.Guilds.GuildStatus.Active
                ));
            });

            modelBuilder.Entity<Tariff>(b =>
            {
                b.ToTable(nameof(Tariff));

                b.HasIndex(t => t.Id)
                    .IsUnique();
                b.HasKey(t => t.Id);
                b.Property(t => t.Id)
                    .HasColumnName("Id")
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

                b.HasOne(l => l.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.TariffId);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable(nameof(User));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .IsRequired();

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.UpdateDate);
                b.Property(g => g.DeletedDate)
                    .IsRequired(false);

                b.Property(g => g.DateOfBirth)
                    .HasDefaultValue(new DateTime(1900, 1, 1))
                    .IsRequired();

                b.Property(g => g.Name)
                    .HasMaxLength(64);

                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(g => g.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.Property(g => g.Login)
                    .IsRequired()
                    .HasMaxLength(64);
                b.Property(g => g.Password)
                    .HasMaxLength(128);

                b.Property(g => g.Roles)
                    .HasMaxLength(512);


                b.HasMany(g => g.Characters)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.HasMany(g => g.Loans)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.HasMany(g => g.Penalties)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.HasMany(g => g.Operations)
                    .WithOne()
                    .HasForeignKey(_ => _.UserId)
                    .HasPrincipalKey(_ => _.Id);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

                b.HasData(new User(
                    id: Guid.NewGuid(),
                    login: "Admin",
                    rank: Types.Gamer.GamerRank.Leader,
                    guildId: new Guid("00000000-0000-4000-0000-000000000001"),
                    roles: "[\"admin\"]",
                    name: "Admin",
                    status: Types.Gamer.GamerStatus.Active,
                    createDate: new DateTime(2020, 01, 30, 7, 35, 9, DateTimeKind.Utc),
                    updateDate: new DateTime(2020, 01, 30, 7, 35, 9, DateTimeKind.Utc),
                    dateOfBirth: new DateTime(2020, 01, 30, 7, 35, 9, DateTimeKind.Utc)
                ));

            });

            modelBuilder.Entity<Character>(b =>
            {
                b.ToTable(nameof(Character));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(g => g.ClassName)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(g => g.Name)
                    .HasMaxLength(64)
                    .IsRequired();
                b.Property(t => t.IsMain)
                    .HasDefaultValue(false)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasMaxLength(32)
                    .HasConversion<String>();
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

                b.Property(l => l.CreateDate)
                    .IsRequired();
                b.Property(l => l.BorrowDate)
                    .IsRequired();
                b.Property(l => l.UpdateDate);
                b.Property(l => l.ExpiredDate)
                    .IsRequired();
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.LoanStatus)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();
                b.Property(l => l.PenaltyAmount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.TaxAmount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.Description)
                    .HasMaxLength(1024);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

                b.HasOne(l => l.User)
                    .WithMany(_ => _.Loans)
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.HasOne(l => l.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.TariffId);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
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

                b.Property(l => l.CreateDate)
                    .IsRequired();
                b.Property(o => o.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();
                b.Property(l => l.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();
                b.Property(l => l.TaxTariff)
                    .HasMaxLength(4096)
                    .HasDefaultValue("{}")
                    .IsRequired();

                b.HasOne(l => l.User)
                    .WithMany(_ => _.Taxs)
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

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
                    .WithMany(_ => _.Penalties)
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
            });

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
                b.Property(o => o.UserId)
                    .IsRequired();

                b.HasOne(g => g.User)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.UserId);

                b.Property(o => o.Ip)
                    .HasMaxLength(128);
                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}