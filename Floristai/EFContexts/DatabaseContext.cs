using Floristai.Entities;
using Microsoft.EntityFrameworkCore;

namespace Floristai.EFContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { 
        }

        public DbSet<DtoFlower> Flowers { get; set; }
        public DbSet<DtoUser> Users { get; set; }
    
    }
}
