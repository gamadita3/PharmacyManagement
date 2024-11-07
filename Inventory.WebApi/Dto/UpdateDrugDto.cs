using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Dto
{
    public class UpdateDrugDto
    {
        [Required]
        public Guid DrugId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public Guid ManufacturerId { get; set; }
    }
}
