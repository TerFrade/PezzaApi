using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using PezzaApi.User.DTO;
using PezzaApi.User.Interfaces;

namespace PezzaApi.User.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly PezzaDbContext dbContext;

        public CustomerHandler(PezzaDbContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomers()
        {
            var customers = await dbContext.Customers.ToListAsync();
            return customers.Select(p => new CustomerDTO(p));
        }

        public async Task<CustomerDTO> GetCustomerById(Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found");
            return new CustomerDTO(customer);
        }

        public async Task<CustomerDTO> CreateCustomer(CustomerDTO customerDTO)
        {
            ValidateCustomer(customerDTO);

            var customer = new Customer
            {
                Name = customerDTO.Name,
                Address = customerDTO.Address,
                Email = customerDTO.Email,
                Cellphone = customerDTO.Cellphone,
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            return new CustomerDTO(customer);
        }

        public async Task UpdateCustomer(CustomerDTO customerDTO)
        {
            ValidateCustomer(customerDTO);

            var customer = await dbContext.Customers.FindAsync(customerDTO.Id);
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            customer.Name = customerDTO.Name;
            customer.Address = customerDTO.Address;
            customer.Email = customerDTO.Email;
            customer.Cellphone = customerDTO.Cellphone;

            dbContext.Entry(customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
        }

        private void ValidateCustomer(CustomerDTO customerDTO)
        {
            // Business logic: Validation
            if (string.IsNullOrWhiteSpace(customerDTO.Name))
            {
                throw new ArgumentException("Customer name is required");
            }

            if (string.IsNullOrWhiteSpace(customerDTO.Email))
            {
                throw new ArgumentException("Customer email is required");
            }

            if (CustomerExists(customerDTO.Email))
            {
                throw new ArgumentException("Customer with the same email already exists.");
            }
        }

        private bool CustomerExists(string email)
        {
            return dbContext.Customers.Any(e => e.Email == email);
        }
    }
}