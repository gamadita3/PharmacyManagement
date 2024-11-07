using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Dto
{
    public class UpdateManufacturerDto
    {
        [Required]
        public Guid ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }
    }
}
