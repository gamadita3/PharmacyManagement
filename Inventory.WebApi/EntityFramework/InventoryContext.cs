using Microsoft.EntityFrameworkCore;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.EntityFramework
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drug>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalPrice)
                .HasPrecision(18, 2);

            // Other configurations
            modelBuilder.Entity<Drug>()
                .HasOne(d => d.Manufacturer)
                .WithMany(m => m.Drugs)
                .HasForeignKey(d => d.ManufacturerId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Drug)
                .WithMany()
                .HasForeignKey(i => i.DrugId);
        }
    }
}
