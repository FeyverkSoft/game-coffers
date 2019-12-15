﻿using System;
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

                b.HasOne(t => t.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Users)
                    .WithOne(_ => _.Guild)
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Operations)
                    .WithOne()
                    .HasForeignKey(_ => _.GuildId)
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();

                b.HasData(new Guild
                {
                    Id = new Guid("00000000-0000-4000-0000-000000000001"),
                    RecruitmentStatus = Types.Guilds.RecruitmentStatus.Close,
                    Name = "Admins",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Status = Types.Guilds.GuildStatus.Active
                });
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

            modelBuilder.Entity<GuildTariff>(b =>
            {
                b.ToTable(nameof(GuildTariff));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.CreateDate)
                    .IsRequired();

                b.HasOne(t => t.BeginnerTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.LeaderTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.OfficerTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.SoldierTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
                b.HasOne(t => t.VeteranTariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable(nameof(User));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
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
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Loans)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Penalties)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Operations)
                    .WithOne()
                    .HasForeignKey(_ => _.UserId)
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();

                b.HasData(new User
                {
                    Id = Guid.NewGuid(),
                    Login = "Admin",
                    Rank = Types.Gamer.GamerRank.Leader,
                    GuildId = new Guid("00000000-0000-4000-0000-000000000001"),
                    Roles = "[\"admin\"]",
                    Name = "Admin",
                    Status = Types.Gamer.GamerStatus.Active,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    DateOfBirth = DateTime.UtcNow
                });

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
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(l => l.Tariff)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);
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
                    .HasPrincipalKey(_ => _.Id);

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
                b.HasOne(_ => _.Guild)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.GuildId);

                b.Property(o => o.ParentOperationId)
                    .IsRequired(false);
                b.HasOne(g => g.ParentOperation)
                    .WithMany()
                    .HasForeignKey(_ => _.ParentOperationId)
                    .HasPrincipalKey(_ => _.Id);

                b.HasOne(g => g.User)
                    .WithMany()
                    .HasForeignKey(_ => _.UserId)
                    .HasPrincipalKey(_ => _.Id);
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

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}