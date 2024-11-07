namespace Inventory.WebApi.Models
{
    public class InvoiceModel
    {
        public Guid InvoiceId { get; set; } = Guid.NewGuid();
        public Guid DrugId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation property
        public DrugModel Drug { get; set; }
    }
}
