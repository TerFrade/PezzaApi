using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key, Column(Order = 1)] public int Id { get; set; }

        [MinLength(3), MaxLength(35), Required] public string Name { get; set; }

        public string? Description { get; set; }

        [Required] public decimal Price { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;

        public static Pizza[] Seed = new Pizza[]
        {
             new Pizza() {Id=1, Name = "Triple Cheese Pizza", DateCreated = DateTime.UtcNow, Description="Triple Cheese Pizza consists of 3 types of cheese: Mozzerella, Cheddar, Feta", Price =  39.99M},
        };
    }
}