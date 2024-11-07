using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Models
{
    public class DrugModel
    {
        public Guid DrugId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Foreign key and navigation property
        public Guid ManufacturerId { get; set; }
        public ManufacturerModel Manufacturer { get; set; }
    }
}
