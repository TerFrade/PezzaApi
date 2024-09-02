using DataAccess.Models;
using System.Net;
using System.Xml.Linq;

namespace PezzaApi.User.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginDTO()
        {
        }

        public LoginDTO(Customer customer)
        {
            Email = customer.Email;
            Password = customer.Password;
        }
    }
}