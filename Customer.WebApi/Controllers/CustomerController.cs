using Customer.WebApi.Dto;
using Customer.WebApi.Models;
using Customer.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerManagement _customerService;

        public CustomersController(ICustomerManagement customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null) return NotFound();

            return customer;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> SearchCustomersByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var customers = await _customerService.SearchCustomersByName(name);
            if (customers == null || !customers.Any())
            {
                return NotFound($"No customers found with name containing '{name}'.");
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomer(AddCustomerDto customerDto)
        {
            var createdCustomer = await _customerService.AddCustomer(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, UpdateCustomerDto customerDto)
        {
            if (id != customerDto.CustomerId) return BadRequest();

            var updatedCustomer = await _customerService.UpdateCustomer(id, customerDto);
            if (updatedCustomer == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var deleted = await _customerService.RemoveCustomer(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
