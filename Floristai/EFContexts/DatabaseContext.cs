using Floristai.Entities;
using Microsoft.EntityFrameworkCore;

namespace Floristai.EFContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { 
        }

        public DbSet<FlowerEntity> Flowers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderLineEntity> OrderLines { get; set; }
        public DbSet<LoggingEntity> Logging { get; set; }

    }
}
