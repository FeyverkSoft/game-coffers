﻿// <auto-generated />
using System;
using Coffers.DB.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coffers.DB.Migrations.Migrations
{
    [DbContext(typeof(MigrateDbContext))]
    partial class MigrateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<bool>("IsMain")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Guild", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("RecruitmentStatus")
                        .IsRequired()
                        .HasColumnName("RecruitmentStatus")
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Guild");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-4000-0000-000000000001"),
                            ConcurrencyTokens = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreateDate = new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc),
                            Name = "Admins",
                            RecruitmentStatus = "Close",
                            Status = "Active",
                            UpdateDate = new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LoanStatus")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<decimal>("PenaltyAmount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<Guid?>("TariffId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TaxAmount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TariffId");

                    b.HasIndex("UserId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Nest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GuildId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Nest");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.NestContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("NestId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("NestId");

                    b.HasIndex("UserId");

                    b.ToTable("NestContract");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GuildId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ParentOperationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Penalty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2048);

                    b.Property<string>("PenaltyStatus")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Penalty");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SessionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("SessionId");

                    b.HasIndex("SessionId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Tariff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("ExpiredLoanTax")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("LoanTax")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Tax")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(4096)
                        .HasDefaultValue("{}");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Tariff");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(65,30)")
                        .HasDefaultValue(0m);

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<string>("TaxTariff")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(4096)
                        .HasDefaultValue("{}");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfBirth")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("GuildId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<string>("Roles")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("71a0603d-3c58-4f13-997f-7861240d9b20"),
                            ConcurrencyTokens = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreateDate = new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc),
                            DateOfBirth = new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc),
                            GuildId = new Guid("00000000-0000-4000-0000-000000000001"),
                            Login = "Admin",
                            Name = "Admin",
                            Rank = "Leader",
                            Roles = "[\"admin\"]",
                            Status = "Active",
                            UpdateDate = new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.UserRole", b =>
                {
                    b.Property<string>("UserRoleId")
                        .HasColumnName("Id")
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<Guid>("GuildId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserRoleId", "GuildId");

                    b.HasIndex("GuildId");

                    b.HasIndex("TariffId");

                    b.HasIndex("UserRoleId", "GuildId")
                        .IsUnique();

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Character", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.User", null)
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Loan", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId");

                    b.HasOne("Coffers.DB.Migrations.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Nest", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Guild", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.NestContract", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Nest", "Nest")
                        .WithMany()
                        .HasForeignKey("NestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coffers.DB.Migrations.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Operation", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Guild", null)
                        .WithMany("Operations")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coffers.DB.Migrations.Entities.User", null)
                        .WithMany("Operations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Penalty", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.User", "User")
                        .WithMany("Penalties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Session", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Tax", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.User", "User")
                        .WithMany("Taxs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.User", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Guild", "Guild")
                        .WithMany("Users")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.UserRole", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Guild", null)
                        .WithMany("Roles")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
