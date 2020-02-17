using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        /// <summary>
        /// 
        /// </summary>
        public TinyCrmDbContext() : base()
        {
            connectionString_ = "Server=localhost;Database=tinycrm;User id=sa;Password=QWE123!@#";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// <summary>
            /// Product Model Builder
            /// </summary>
            modelBuilder
                .Entity<Product>()
                .ToTable("Product");

            modelBuilder
                .Entity<Product>()
                .Property(c => c.Id)
                .IsRequired();            
            
            modelBuilder
                .Entity<Product>()
                .Property(c => c.Name)
                .IsRequired();            
            
            modelBuilder
                .Entity<Product>()
                .Property(c => c.Price)
                .IsRequired();            
            
            modelBuilder
                .Entity<Product>()
                .Property(c => c.Category)
                .IsRequired();

            /// <summary>
            /// Customer Model Builder
            /// </summary>
            modelBuilder
                .Entity<Customer>()
                .ToTable("Customer");              
            
            modelBuilder
                .Entity<Customer>()
                .Property(c => c.VatNumber)
                .HasMaxLength(9)
                .IsRequired();             
            
            modelBuilder
                .Entity<Customer>()
                .HasIndex(c => c.VatNumber)
                .IsUnique();

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired();            
            
            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Status)
                .IsRequired();

            /// <summary>
            /// Order Model Builder
            /// </summary>            
            modelBuilder
                .Entity<Country>()
                .ToTable("Country");            
            
            modelBuilder
                .Entity<Country>()
                .Property(c => c.CountryId)
                .IsRequired();

            modelBuilder
                .Entity<Country>()
                .HasIndex(c => c.CountryId)
                .IsUnique();

            /// <summary>
            /// Order Model Builder
            /// </summary>
            modelBuilder
                .Entity<Order>()
                .ToTable("Order");

            /// <summary>
            /// Contact Model Builder
            /// </summary>
            modelBuilder
                .Entity<ContactPerson>()
                .ToTable("ContactPerson");            
            
            modelBuilder
                .Entity<OrderProduct>()
                .ToTable("OrderProduct");            
            
            modelBuilder
                .Entity<OrderProduct>()
                .HasKey(key => new { key.OrderId, key.ProductId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
