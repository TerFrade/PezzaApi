using Microsoft.EntityFrameworkCore;
using PezzaApi.Common.Interfaces;
using PezzaApi.Data;
using PezzaApi.Data.Models;
using PezzaApi.DTO;

public class PizzaHandler : IPizzaHandler
{
    private readonly PezzaDbContext _dbContext;

    public PizzaHandler(PezzaDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<PezzaApi.DTO.Pizza>> GetPizzas()
    {
        var pizzas = await _dbContext.Pizza.ToListAsync();
        return pizzas.Select(p => new Pizza(p));
    }

    public async Task<PezzaApi.DTO.Pizza> GetPizzaById(Guid id)
    {
        var pizza = await _dbContext.Pizza.FindAsync(id);
        if (pizza == null) throw new ArgumentException("Pizza not found");
        return new Pizza(pizza);
    }

    public async Task<PezzaApi.DTO.Pizza> CreatePizza(PezzaApi.DTO.Pizza pizzaDTO)
    {
        var pizza = new PezzaApi.Data.Models.PizzaModel
        {
            Id = pizzaDTO.Id,
            Name = pizzaDTO.Name,
            Description = pizzaDTO.Description,
            Price = pizzaDTO.Price,
            DateCreated = DateTime.Now
        };

        _dbContext.Pizza.Add(pizza);
        await _dbContext.SaveChangesAsync();

        return new Pizza(pizza);
    }

    public async Task UpdatePizza(Guid id, PezzaApi.DTO.Pizza pizzaDTO)
    {
        var pizza = await _dbContext.Pizza.FindAsync(id);
        if (pizza == null) throw new ArgumentException("Pizza not found");

        // Update properties
        pizza.Name = pizzaDTO.Name;
        pizza.Description = pizzaDTO.Description;
        pizza.Price = pizzaDTO.Price;
        pizza.DateCreated = DateTime.Now;

        _dbContext.Entry(pizza).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePizza(Guid id)
    {
        var pizza = await _dbContext.Pizza.FindAsync(id);
        if (pizza != null)
        {
            _dbContext.Pizza.Remove(pizza);
            await _dbContext.SaveChangesAsync();
        }
    }
}