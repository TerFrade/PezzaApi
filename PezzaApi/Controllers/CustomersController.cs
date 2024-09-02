using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PezzaApi.User.DTO;
using PezzaApi.User.Interfaces;

namespace PezzaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerHandler handler;

        public CustomersController(ICustomerHandler customerHandler)
        {
            handler = customerHandler;
        }

        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> GetCustomers()
        {
            var customersDTOs = await handler.GetCustomers();
            return Ok(customersDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(Guid id)
        {
            var pizzaDTO = await handler.GetCustomerById(id);
            return Ok(pizzaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customer)
        {
            await handler.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            var createdPizza = await handler.CreateCustomer(customerDTO);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdPizza.Id }, createdPizza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await handler.DeleteCustomer(id);
            return NoContent();
        }
    }
}