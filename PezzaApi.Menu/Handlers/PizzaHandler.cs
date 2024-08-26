using DataAccess;
using Microsoft.EntityFrameworkCore;
using PezzaApi.DataAccess.Models;
using PezzaApi.Menu.DTO;
using PezzaApi.Menu.Interfaces;

public class PizzaHandler : IPizzaHandler
{
    private readonly PezzaDbContext dbContext;

    public PizzaHandler(PezzaDbContext context)
    {
        dbContext = context;
    }

    public async Task<IEnumerable<PizzaDTO>> GetPizzas()
    {
        var pizzas = await dbContext.Pizzas.ToListAsync();
        return pizzas.Select(p => new PizzaDTO(p));
    }

    public async Task<PizzaDTO> GetPizzaById(Guid id)
    {
        var pizza = await dbContext.Pizzas.FindAsync(id);
        if (pizza == null) throw new ArgumentException("Pizza not found");
        return new PizzaDTO(pizza);
    }

    public async Task<PizzaDTO> CreatePizza(PizzaDTO pizzaDTO)
    {
        ValidatePizza(pizzaDTO);

        var pizza = new Pizza
        {
            Name = pizzaDTO.Name,
            Description = pizzaDTO.Description,
            Price = pizzaDTO.Price,
        };

        dbContext.Pizzas.Add(pizza);
        await dbContext.SaveChangesAsync();

        return new PizzaDTO(pizza);
    }

    public async Task UpdatePizza(PizzaDTO pizzaDTO)
    {
        ValidatePizza(pizzaDTO);

        var pizza = await dbContext.Pizzas.FindAsync(pizzaDTO.Id);
        if (pizza == null) throw new ArgumentException("Pizza not found");

        pizza.Name = pizzaDTO.Name;
        pizza.Description = pizzaDTO.Description;
        pizza.Price = pizzaDTO.Price;

        dbContext.Entry(pizza).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeletePizza(Guid id)
    {
        var pizza = await dbContext.Pizzas.FindAsync(id);
        if (pizza == null) throw new ArgumentException("Pizza not found");

        dbContext.Pizzas.Remove(pizza);
        await dbContext.SaveChangesAsync();
    }

    private void ValidatePizza(PizzaDTO pizzaDTO)
    {
        // Business logic: Validation
        if (string.IsNullOrWhiteSpace(pizzaDTO.Name))
        {
            throw new ArgumentException("Pizza name is required");
        }

        if (pizzaDTO.Price < 20)
        {
            throw new ArgumentException("Pizza price needs to be more than R20");
        }
    }
}