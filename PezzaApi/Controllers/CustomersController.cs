using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Models;
using PezzaApi.User.DTO;

namespace PezzaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly PezzaDbContext dbContext;

        public CustomersController(PezzaDbContext context)
        {
            dbContext = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IEnumerable<CustomerDTO>> GetCustomers()
        {
            var customers = await dbContext.Customers.ToListAsync();
            return customers.Select(p => new CustomerDTO(p));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return new CustomerDTO(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Name = customerDTO.Name,
                Address = customerDTO.Address,
                Email = customerDTO.Email,
                Cellphone = customerDTO.Cellphone,
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return dbContext.Customers.Any(e => e.Id == id);
        }
    }
}