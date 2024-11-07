using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Dto
{
    public class AddManufacturerDto
    {
        [Required]
        public string Name { get; set; }

        public string Country { get; set; }
    }
}
