using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using PezzaApi.Services;
using PezzaApi.User.DTO;
using PezzaApi.User.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace PezzaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService tokenService;
        private readonly PasswordService passwordService;
        private readonly ICustomerHandler customerHandler;

        public AuthController(TokenService _tokenService, PasswordService _passwordService, ICustomerHandler _customerHandler)
        {
            tokenService = _tokenService;
            passwordService = _passwordService;
            customerHandler = _customerHandler;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = customerHandler.CustomerExists(loginDTO.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            if (passwordService.VerifyPassword(user.Password, loginDTO.Password))
            {
                // Generate JWT token
                var token = tokenService.GenerateToken(loginDTO.Email.ToLower(), user.Role);

                return Ok(new
                {
                    token = token,
                    expiration = new JwtSecurityTokenHandler().ReadToken(token).ValidTo
                });
            }

            // If authentication fails, return Unauthorized response
            return Unauthorized(new { message = "Authentication failed." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user already exists

            var existingCustomer = customerHandler.CustomerExists(registerDTO.Email);

            if (existingCustomer != null)
            {
                return Unauthorized(new { message = "Customer already exists." });
            }

            // Hash the user's password
            var passwordHash = passwordService.HashPassword(registerDTO.Password);

            // Create a new Customer object
            var newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = passwordHash,
                Address = registerDTO.Address,
                Cellphone = registerDTO.Cellphone,
                Role = UserRole.User // or default role
            };

            await customerHandler.AddCustomer(newCustomer);

            return Ok(new CustomerDTO(newCustomer));
        }
    }
}