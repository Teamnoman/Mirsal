using Microsoft.AspNetCore.Mvc;
using MIRSAL.DTO;
using MIRSAL.Services;

namespace MIRSAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(CustomerService authService) : ControllerBase
    {
        private readonly CustomerService _authService = authService;

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto Login)
        {
            var customer = await _authService.GetCustomerByUsername(Login.Username);

            if (customer == null)
            {
                return NotFound();
            }

            if (Login.Password == customer.Password){

                var customeToReturn = new CustomerToReturnDto {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Username = customer.Username,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    CreatedDate = customer.CreatedDate
                };

                return Ok(customeToReturn);
            }

            return NotFound();
        }
    }
}   