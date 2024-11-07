namespace Inventory.WebApi.Models
{
    public class ManufacturerModel
    {
        public Guid ManufacturerId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Country { get; set; }

        // Navigation property for related drugs
        public ICollection<DrugModel> Drugs { get; set; }
    }
}
