using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PezzaApi.Services;
using PezzaApi.User.DTO;

namespace PezzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public PizzaController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("This is a public endpoint");
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("This is a protected endpoint, accessible with a valid JWT token");
        }

        [HttpPost("login")]
        public IActionResult Login(CustomerDTO login)
        {
            try
            {
                if (IsValidUser(login))
                {
                    var token = _tokenService.GenerateToken(login.Email);
                    return Ok(new { token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                // Log the error details using your preferred logging mechanism
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool IsValidUser(CustomerDTO login)
        {
            // Implement your user validation logic here
            return true;
        }
    }
}
