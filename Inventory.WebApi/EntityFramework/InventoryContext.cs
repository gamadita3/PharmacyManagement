using Microsoft.EntityFrameworkCore;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.EntityFramework
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<DrugModel> Drugs { get; set; }
        public DbSet<ManufacturerModel> Manufacturers { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrugModel>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceModel>()
                .Property(i => i.TotalPrice)
                .HasPrecision(18, 2);

            // Other configurations
            modelBuilder.Entity<DrugModel>()
                .HasOne(d => d.Manufacturer)
                .WithMany(m => m.Drugs)
                .HasForeignKey(d => d.ManufacturerId);

            modelBuilder.Entity<InvoiceModel>()
                .HasOne(i => i.Drug)
                .WithMany()
                .HasForeignKey(i => i.DrugId);
        }
    }
}
