using PezzaApi.Data.Models;

namespace PezzaApi.DTO
{
    public class PizzaDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public PizzaDTO()
        { }

        public PizzaDTO(Pizza pizza)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            Description = pizza.Description;
            Price = pizza.Price;
        }
    }
}