using PezzaApi.User.DTO;

namespace PezzaApi.User.Interfaces
{
    public interface ICustomerHandler
    {
        Task<IEnumerable<CustomerDTO>> GetCustomers();

        Task<CustomerDTO> GetCustomerById(Guid id);

        Task UpdateCustomer(CustomerDTO customerDTO);

        Task<CustomerDTO> CreateCustomer(CustomerDTO customerDTO);

        Task DeleteCustomer(Guid id);
    }
}