using Customer.WebApi.Models;
using Customer.WebApi.Dto;


namespace Customer.WebApi.Services
{
    public interface ICustomerManagement
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomers();
        Task<CustomerModel> GetCustomerById(Guid id);
        Task<IEnumerable<CustomerModel>> SearchCustomersByName(string name);
        Task<CustomerModel> AddCustomer(AddCustomerDto customerDto);
        Task<CustomerModel> UpdateCustomer(Guid id, UpdateCustomerDto customerDto);
        Task<bool> RemoveCustomer(Guid id);
    }
}
