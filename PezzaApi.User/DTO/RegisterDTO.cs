using DataAccess.Models;

namespace PezzaApi.User.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterDTO()
        {
        }

        public RegisterDTO(Customer customer)
        {
            Name = customer.Name;
            Address = customer.Address;
            Cellphone = customer.Cellphone;
            Email = customer.Email;
            Password = customer.Password;
        }
    }
}