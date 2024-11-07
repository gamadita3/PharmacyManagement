using System.ComponentModel.DataAnnotations;

namespace Customer.WebApi.Dto
{
    public class UpdateCustomerDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
