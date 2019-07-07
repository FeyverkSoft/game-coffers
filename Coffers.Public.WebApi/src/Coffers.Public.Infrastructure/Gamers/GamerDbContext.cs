using System;
using Coffers.Public.Domain.Gamers;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Gamers
{

    public class GamerDbContext  : DbContext
    {
        public DbSet<Gamer> Gamers { get; set; }

        public GamerDbContext(DbContextOptions<GamerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gamer>(b =>
            {
                b.ToTable(nameof(Gamer));

                b.HasIndex(o => o.Id)
                    .IsUnique();
                b.HasKey(o => o.Id);
                b.Property(o => o.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                b.Property(o => o.Status)
                    .HasConversion<String>()
                    .IsRequired();

            });
        }
    }
}
