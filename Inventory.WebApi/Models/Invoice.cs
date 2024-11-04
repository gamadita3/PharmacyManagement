namespace Inventory.WebApi.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; } = Guid.NewGuid();
        public Guid DrugId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Drug Drug { get; set; }
    }
}
