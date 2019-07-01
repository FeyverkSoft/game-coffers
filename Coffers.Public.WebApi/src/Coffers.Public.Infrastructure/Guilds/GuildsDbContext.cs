using Coffers.Public.Domain.Guilds;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Guilds
{
    public class GuildsDbContext : DbContext
    {
        public DbSet<Guild> Guilds { get; set; }

        public GuildsDbContext(DbContextOptions<GuildsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Guild>(builder =>
            {
                builder.ToTable("Guilds");
                builder.HasKey(g => g.Id);
                builder.Property(g => g.Id)
                    .HasColumnName("GuildId")
                    .IsRequired();

                builder.Property(client => client.CreateDate)
                    .HasColumnName("CreateDate")
                    .IsRequired();
                builder.Property(client => client.UpdateDate)
                    .HasColumnName("UpdateDate")
                    .IsRequired();
                builder.Property(client => client.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
        }
    }
}
