using Customer.WebApi.Dto;
using Customer.WebApi.EntityFramework;
using Customer.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.WebApi.Services
{
    public class CustomerManagement : ICustomerManagement
    {
        private readonly CustomerContext _context;

        public CustomerManagement(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<CustomerModel> GetCustomerById(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<CustomerModel>> SearchCustomersByName(string name)
        {
            return await _context.Customers
                .Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name))
                .ToListAsync();
        }

        public async Task<CustomerModel> AddCustomer(AddCustomerDto customerDto)
        {
            var customer = new CustomerModel
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                DateOfBirth = customerDto.DateOfBirth,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Address = customerDto.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<CustomerModel> UpdateCustomer(Guid id, UpdateCustomerDto customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.Address = customerDto.Address;
            customer.IsActive = customerDto.IsActive;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> RemoveCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
