﻿using System;
using Coffers.Public.Domain.Gamers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coffers.Public.Infrastructure.Gamers
{

    public class GamerDbContext : DbContext
    {
        public DbSet<Gamer> Gamers { get; set; }

        public GamerDbContext(DbContextOptions<GamerDbContext> options) : base(options) { }

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

                b.Property(g => g.UpdateDate)
                    .HasDefaultValue(new DateTime(1900, 1, 1))
                    .IsRequired();

                b.Property(g => g.Rank)
                    .HasConversion<String>()
                    .HasMaxLength(32);
                b.Property(g => g.Status)
                    .HasConversion<String>()
                    .HasMaxLength(32);

                b.HasOne(g => g.DefaultAccount)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

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

                b.Property(a => a.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
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
                b.Property(t => t.IsMain)
                    .HasConversion(new BoolToZeroOneConverter<Boolean>())
                    .HasDefaultValue(false)
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

                b.HasOne(_ => _.Account)
                    .WithMany()
                    .HasPrincipalKey(_ => _.Id);

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
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

                b.Property(t => t.PenaltyStatus)
                    .HasConversion<String>()
                    .IsRequired();

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });
        }
    }
}
