using Customer.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.WebApi.EntityFramework
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<CustomerModel> Customers { get; set; }
    }
}
