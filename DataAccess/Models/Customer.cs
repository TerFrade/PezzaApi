using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Customer
    {
        [Key] public Guid Id { get; set; }

        [MaxLength(35), Required] public string Name { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Cellphone { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}