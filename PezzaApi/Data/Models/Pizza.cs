using System.ComponentModel.DataAnnotations;

namespace PezzaApi.Data.Models
{
    public class Pizza
    {
        [Key] public Guid Id { get; set; }
        [MinLength(3), MaxLength(25), Required] public string Name { get; set; }
        public string? Description { get; set; }
        [Required] public decimal Price { get; set; }
        public DateTime? DateCreated { get; set; }

        public static Pizza[] Seed = new Pizza[]
        {
             new Pizza() { Id = Guid.NewGuid(), Name = "Triple Cheese Pizza", DateCreated = DateTime.Now, Description="Triple Cheese Pizza consists of 3 types of cheese: Mozzerella, Cheddar, Feta", Price =  39.99M},
        };
    }
}