﻿using Microsoft.AspNetCore.Mvc;
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
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
        return NoContent();
    }

    // POST: api/Pizzas
    [HttpPost]
    [Produces(typeof(PizzaDTO))]
    public async Task<ActionResult> PostPizza(PizzaDTO pizzaDTO)
    {
        var pizza = await handler.CreatePizza(pizzaDTO);
        return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
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