using ClaimBasedAuthorization.Models;
using ClaimBasedAuthorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        // GET api/values
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var token = await authService.Authenticate(user.UserName, user.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }

        }
    }
}
