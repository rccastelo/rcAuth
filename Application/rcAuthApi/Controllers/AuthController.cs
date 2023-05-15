using Microsoft.AspNetCore.Mvc;
using rcAuthApplication.Interfaces;
using rcAuthApplication.Transport;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace rcAuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Autenticação de usuário",
            Description = "[pt-BR] Autenticação de usuário. \n\n " +
                "[en-US] User authentication. ",
            Tags = new[] { "Auth" }
        )]
        [ProducesResponseType(typeof(AuthRequest), 200)]
        [ProducesResponseType(typeof(AuthRequest), 400)]
        [ProducesResponseType(500)]
        public IActionResult Login(AuthRequest authRequest)
        {
            AuthResponse response = null;

            try {
                response = _authService.Login(authRequest);
            } catch {
                response = new AuthResponse();
                response.IsValid = false;
                response.IsError = true;
                response.AddMessage("Não foi possível autenticar o usuário");
            }

            if (response.IsError || !response.IsValid) {
                return BadRequest(response);
            } else {
                return Ok(response);
            }
        }
    }
}
