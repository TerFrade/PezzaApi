using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PezzaApi.Data;
using PezzaApi.Data.Models;
using PezzaApi.DTO;

namespace PezzaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly PezzaDbContext _context;

        public PizzasController(PezzaDbContext context)
        {
            _context = context;
        }

        // GET: api/Pizzas
        [HttpGet]
        [Produces(typeof(PizzaDTO))]
        public async Task<IActionResult> GetPizza()
        {
            var pizzas = await _context.Pizza.ToListAsync();
            var pizzaDTOs = pizzas.Select(x => new PizzaDTO(x)).ToList();
            return Ok(pizzaDTOs);
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        [Produces(typeof(PizzaDTO))]
        public async Task<ActionResult<PizzaDTO>> GetPizza(Guid id)
        {
            var pizza = await _context.Pizza.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            var pizzaDTO = new PizzaDTO(pizza);
            return Ok(pizzaDTO);
        }

        // PUT: api/Pizzas/5
        [HttpPut("{id}")]
        [Produces(typeof(PizzaDTO))]
        public async Task<IActionResult> PutPizza(Guid id, PizzaDTO pizzaDTO)
        {
            if (id != pizzaDTO.Id)
            {
                return BadRequest();
            }

            var pizza = await _context.Pizza.FirstOrDefaultAsync(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            // Map PizzaDTO to Pizza entity
            pizza.Name = pizzaDTO.Name;
            pizza.Description = pizzaDTO.Description;
            pizza.Price = pizzaDTO.Price;

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pizzas
        [HttpPost]
        [Produces(typeof(PizzaDTO))]
        public async Task<ActionResult<PizzaDTO>> PostPizza([FromBody] PizzaDTO pizzaDTO)
        {
            var pizza = new Pizza
            {
                Id = pizzaDTO.Id,
                Name = pizzaDTO.Name,
                Description = pizzaDTO.Description,
                Price = pizzaDTO.Price,
                DateCreated = DateTime.Now,
            };

            _context.Pizza.Add(pizza);
            await _context.SaveChangesAsync();

            pizzaDTO.Id = pizza.Id;
            return CreatedAtAction(nameof(GetPizza), new { id = pizzaDTO.Id }, pizzaDTO);
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        [Produces(typeof(PizzaDTO))]
        public async Task<IActionResult> DeletePizza(Guid id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(Guid id)
        {
            return _context.Pizza.Any(e => e.Id == id);
        }
    }
}