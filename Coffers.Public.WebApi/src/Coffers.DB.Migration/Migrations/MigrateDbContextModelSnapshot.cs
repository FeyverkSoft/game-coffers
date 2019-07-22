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
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Account", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<byte[]>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Character", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<byte[]>("GamerId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("GamerId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Gamer", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DateOfBirth")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<byte[]>("DefaultAccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<byte[]>("GuildId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.Property<string>("Password")
                        .HasMaxLength(128);

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Roles")
                        .HasMaxLength(512);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DefaultAccountId");

                    b.HasIndex("GuildId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Gamer");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Guild", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDate");

                    b.Property<byte[]>("GuildAccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasMaxLength(512);

                    b.Property<string>("RecruitmentStatus")
                        .IsRequired()
                        .HasColumnName("RecruitmentStatus")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasMaxLength(32);

                    b.Property<byte[]>("TariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("GuildAccountId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TariffId");

                    b.ToTable("Guild");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.GuildTariff", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<byte[]>("BeginnerTariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("CreateDate");

                    b.Property<byte[]>("LeaderTariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("OfficerTariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("SoldierTariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("VeteranTariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("BeginnerTariffId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LeaderTariffId");

                    b.HasIndex("OfficerTariffId");

                    b.HasIndex("SoldierTariffId");

                    b.HasIndex("VeteranTariffId");

                    b.ToTable("GuildTariff");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.History", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<DateTime>("CreateDate");

                    b.Property<byte[]>("GamerId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("GamerId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("History");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Loan", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<byte[]>("AccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("BorrowDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(512);

                    b.Property<DateTime>("ExpiredDate");

                    b.Property<byte[]>("GamerId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("LoanStatus")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<decimal>("PenaltyAmount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<byte[]>("TariffId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<decimal>("TaxAmount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GamerId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TariffId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Operation", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(512);

                    b.Property<byte[]>("DocumentId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("FromAccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("OperationDate");

                    b.Property<byte[]>("ToAccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ToAccountId");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Penalty", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<byte[]>("AccountId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<byte[]>("GamerId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("PenaltyStatus")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GamerId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Penalty");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Session", b =>
                {
                    b.Property<byte[]>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("SessionId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<byte[]>("GamerId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Ip")
                        .HasMaxLength(128);

                    b.HasKey("SessionId");

                    b.HasIndex("GamerId");

                    b.HasIndex("SessionId")
                        .IsUnique();

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Tariff", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)))
                        .HasColumnName("Id");

                    b.Property<decimal>("ExpiredLoanTax")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<decimal>("LoanTax")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<string>("Tax")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4096)
                        .HasDefaultValue("{}");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Tariff");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Character", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Gamer")
                        .WithMany("Characters")
                        .HasForeignKey("GamerId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Gamer", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "DefaultAccount")
                        .WithMany()
                        .HasForeignKey("DefaultAccountId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Guild", "Guild")
                        .WithMany("Gamers")
                        .HasForeignKey("GuildId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Guild", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "GuildAccount")
                        .WithMany()
                        .HasForeignKey("GuildAccountId");

                    b.HasOne("Coffers.DB.Migrations.Entities.GuildTariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.GuildTariff", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "BeginnerTariff")
                        .WithMany()
                        .HasForeignKey("BeginnerTariffId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "LeaderTariff")
                        .WithMany()
                        .HasForeignKey("LeaderTariffId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "OfficerTariff")
                        .WithMany()
                        .HasForeignKey("OfficerTariffId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "SoldierTariff")
                        .WithMany()
                        .HasForeignKey("SoldierTariffId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "VeteranTariff")
                        .WithMany()
                        .HasForeignKey("VeteranTariffId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.History", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Gamer", "Gamer")
                        .WithMany("Histories")
                        .HasForeignKey("GamerId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Loan", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Gamer", "Gamer")
                        .WithMany("Loans")
                        .HasForeignKey("GamerId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Tariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Operation", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "FromAccount")
                        .WithMany("FromOperations")
                        .HasForeignKey("FromAccountId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "ToAccount")
                        .WithMany("ToOperations")
                        .HasForeignKey("ToAccountId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Penalty", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Coffers.DB.Migrations.Entities.Gamer", "Gamer")
                        .WithMany("Penalties")
                        .HasForeignKey("GamerId");
                });

            modelBuilder.Entity("Coffers.DB.Migrations.Entities.Session", b =>
                {
                    b.HasOne("Coffers.DB.Migrations.Entities.Gamer", "Gamer")
                        .WithMany()
                        .HasForeignKey("GamerId");
                });
#pragma warning restore 612, 618
        }
    }
}
