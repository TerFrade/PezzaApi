using Microsoft.AspNetCore.Mvc;
using PezzaApi.Common.Interfaces;
using PezzaApi.DTO;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaHandler handler;

    public PizzaController(IPizzaHandler pizzaHandler)
    {
        handler = pizzaHandler;
    }

    // GET: api/Pizzas
    [HttpGet]
    [Produces(typeof(Pizza))]
    public async Task<IActionResult> GetPizza()
    {
        var pizzaDTOs = await handler.GetPizzas();
        return Ok(pizzaDTOs);
    }

    // GET: api/Pizzas/5
    [HttpGet("{id}")]
    [Produces(typeof(Pizza))]
    public async Task<ActionResult<Pizza>> GetPizza(Guid id)
    {
        var pizzaDTO = await handler.GetPizzaById(id);
        if (pizzaDTO == null)
        {
            return NotFound();
        }
        return Ok(pizzaDTO);
    }

    // PUT: api/Pizzas/5
    [HttpPut("{id}")]
    [Produces(typeof(void))]
    public async Task<IActionResult> PutPizza(Guid id, Pizza pizza)
    {
        try
        {
            await handler.UpdatePizza(id, pizza);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
        return NoContent();
    }

    // POST: api/Pizzas
    [HttpPost]
    [Produces(typeof(Pizza))]
    public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
    {
        var pizzaDTO = await handler.CreatePizza(pizza);
        return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizzaDTO);
    }

    // DELETE: api/Pizzas/5
    [HttpDelete("{id}")]
    [Produces(typeof(void))]
    public async Task<IActionResult> DeletePizza(Guid id)
    {
        await handler.DeletePizza(id);
        return NoContent();
    }
}