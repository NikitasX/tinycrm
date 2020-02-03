using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .ToTable("Product");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=tinycrm;Integrated Security=True;");
        }
    }
}
