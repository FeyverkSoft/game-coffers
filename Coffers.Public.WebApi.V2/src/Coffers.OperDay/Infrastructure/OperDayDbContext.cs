using System;
using Microsoft.EntityFrameworkCore;

namespace Coffers.OperDay.Infrastructure
{
    public class OperDayDbContext : DbContext
    {

        public OperDayDbContext(DbContextOptions<OperDayDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
