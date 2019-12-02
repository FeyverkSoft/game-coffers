using System;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    public class GamerQueryDbContext : DbContext
    {
        public DbSet<Gamer> Gamers { get; set; }

        public GamerQueryDbContext(DbContextOptions<GamerQueryDbContext> options) : base(options) { }

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
                    .HasDefaultValue(false)
                    .IsRequired();
                b.Property(t => t.Name);
                b.Property(t => t.ClassName);
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
                    .HasPrincipalKey(_ => _.Id)
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
