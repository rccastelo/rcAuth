using Microsoft.AspNetCore.Mvc;
using rcAuthApplication.Application;
using rcAuthApplication.Interfaces;

namespace rcAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login(AuthRequest authRequest)
        {
            AuthResponse response = _authService.Login(authRequest);

            return Ok(response);
        }
    }
}
