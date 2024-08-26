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

    [HttpGet]
    [Produces(typeof(PizzaDTO))]
    public async Task<IActionResult> GetPizza()
    {
        var pizzaDTOs = await handler.GetPizzas();
        return Ok(pizzaDTOs);
    }

    [HttpGet("{id}")]
    [Produces(typeof(PizzaDTO))]
    public async Task<ActionResult<PizzaDTO>> GetPizza(Guid id)
    {
        var pizzaDTO = await handler.GetPizzaById(id);
        return Ok(pizzaDTO);
    }

    [HttpPut]
    [Produces(typeof(void))]
    public async Task<IActionResult> UpdatePizza(PizzaDTO pizzaDTO)
    {
        await handler.UpdatePizza(pizzaDTO);
        return NoContent();
    }

    [HttpPost]
    [Produces(typeof(PizzaDTO))]
    public async Task<ActionResult> CreatePizza(PizzaDTO pizzaDTO)
    {
        var createdPizza = await handler.CreatePizza(pizzaDTO);
        return CreatedAtAction(nameof(GetPizza), new { id = createdPizza.Id }, createdPizza);
    }

    [HttpDelete("{id}")]
    [Produces(typeof(void))]
    public async Task<IActionResult> DeletePizza(Guid id)
    {
        await handler.DeletePizza(id);
        return NoContent();
    }
}