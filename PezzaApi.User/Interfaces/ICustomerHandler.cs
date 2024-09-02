using DataAccess.Models;
using PezzaApi.User.DTO;

namespace PezzaApi.User.Interfaces
{
    public interface ICustomerHandler
    {
        Task<IEnumerable<CustomerDTO>> GetCustomers();

        Task<CustomerDTO> GetCustomerById(Guid id);

        Task<CustomerDTO> CreateCustomer(CustomerDTO customerDTO);

        Task AddCustomer(Customer customer);

        Task UpdateCustomer(CustomerDTO customerDTO);

        Task DeleteCustomer(Guid id);

        Customer CustomerExists(string email);
    }
}