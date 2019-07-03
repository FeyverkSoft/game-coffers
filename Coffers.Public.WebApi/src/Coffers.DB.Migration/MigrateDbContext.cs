using Microsoft.EntityFrameworkCore;

namespace Coffers.DB.Migration
{
    public class MigrateDbContext
        : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}