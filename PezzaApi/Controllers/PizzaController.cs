using Microsoft.AspNetCore.Mvc;
using PezzaApi.Menu.DTO;
using PezzaApi.Menu.Interfaces;

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
    [Produces(typeof(PizzaDTO))]
    public async Task<IActionResult> GetPizza()
    {
        var pizzaDTOs = await handler.GetPizzas();
        return Ok(pizzaDTOs);
    }

    // GET: api/Pizzas/5
    [HttpGet("{id}")]
    [Produces(typeof(PizzaDTO))]
    public async Task<ActionResult<PizzaDTO>> GetPizza(Guid id)
    {
        var pizzaDTO = await handler.GetPizzaById(id);
        if (pizzaDTO == null)
        {
            return NotFound();
        }
        return Ok(pizzaDTO);
    }

    // PUT: api/Pizzas/5
    [HttpPut]
    [Produces(typeof(void))]
    public async Task<IActionResult> UpdatePizza(PizzaDTO pizzaDTO)
    {
        try
        {
            await handler.UpdatePizza(pizzaDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Pizzas
    [HttpPost]
    [Produces(typeof(PizzaDTO))]
    public async Task<ActionResult> PostPizza(PizzaDTO pizzaDTO)
    {
        try
        {
            var pizza = await handler.CreatePizza(pizzaDTO);
            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Pizzas/5
    [HttpDelete("{id}")]
    [Produces(typeof(void))]
    public async Task<IActionResult> DeletePizza(Guid id)
    {
        try
        {
            await handler.DeletePizza(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}