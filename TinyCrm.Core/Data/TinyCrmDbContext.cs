using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        public TinyCrmDbContext() : base()
        {
            connectionString_ = "Server=localhost;Database=tinycrm;User id=sa;Password=QWE123!@#";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .ToTable("Product");            
            
            modelBuilder
                .Entity<Customer>()
                .ToTable("Customer");            
            
            modelBuilder
                .Entity<Order>()
                .ToTable("Order");            
            
            modelBuilder
                .Entity<ContactPerson>()
                .ToTable("ContactPerson");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
