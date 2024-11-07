using System.ComponentModel.DataAnnotations;

namespace Customer.WebApi.Models
{
    public class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
