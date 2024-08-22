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
            return Ok(await _context.Pizza.ToListAsync());
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(Guid id)
        {
            var pizza = await _context.Pizza.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces(typeof(PizzaDTO))]
        public async Task<IActionResult> PutPizza(Guid id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces(typeof(PizzaDTO))]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _context.Pizza.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
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