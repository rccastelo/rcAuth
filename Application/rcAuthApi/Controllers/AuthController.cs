using Microsoft.AspNetCore.Mvc;
using rcAuthApplication;
using rcAuthDomain;

namespace rcAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult login(AuthEntity authEntity)
        {
            AuthEntity auth = _authService.login(authEntity);

            return Ok(auth);
        }
    }
}
