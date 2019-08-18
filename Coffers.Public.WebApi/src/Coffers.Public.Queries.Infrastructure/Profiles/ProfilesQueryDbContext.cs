﻿using System;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Queries.Infrastructure.Profiles
{
    public class ProfilesQueryDbContext : DbContext
    {
        public DbSet<Gamer> Gamers { get; set; }

        public ProfilesQueryDbContext(DbContextOptions<ProfilesQueryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Gamer>(b =>
            {
                b.ToTable(nameof(Gamer));

                b.HasIndex(g => g.Id)
                    .IsUnique();
                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .IsRequired();


                b.Property(g => g.Name)
                    .HasMaxLength(64);
                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasOne(g => g.DefaultAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired();

                b.HasMany(g => g.Characters)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Loans)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);

                b.HasMany(g => g.Penalties)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id);
            });

            modelBuilder.Entity<Account>(b =>
            {
                b.ToTable(nameof(Account));

                b.HasIndex(a => a.Id)
                    .IsUnique();
                b.HasKey(a => a.Id);
                b.Property(a => a.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(a => a.Balance)
                    .HasDefaultValue(0)
                    .IsRequired();

                b.HasMany(g => g.FromOperations)
                    .WithOne(_ => _.FromAccount)
                    .HasPrincipalKey(_ => _.Id);

            });

            modelBuilder.Entity<Character>(b =>
            {
                b.ToTable(nameof(Character));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.Status)
                    .HasConversion<String>()
                    .IsRequired();
            });

            modelBuilder.Entity<Operation>(b =>
            {
                b.ToTable(nameof(Operation));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.Type)
                    .HasConversion<String>()
                    .IsRequired();
            });

            modelBuilder.Entity<Loan>(b =>
            {
                b.ToTable(nameof(Loan));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);
                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(t => t.LoanStatus)
                    .HasConversion<String>()
                    .IsRequired();

            });

            modelBuilder.Entity<Penalty>(b =>
            {
                b.ToTable(nameof(Penalty));

                b.HasIndex(gt => gt.Id)
                    .IsUnique();
                b.HasKey(gt => gt.Id);

                b.Property(gt => gt.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(a => a.Amount)
                    .HasDefaultValue(0)
                    .IsRequired();

                b.Property(t => t.PenaltyStatus)
                    .HasConversion<String>()
                    .IsRequired();
            });
        }
    }
}