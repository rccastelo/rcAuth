using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace rcAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Testar resposta da API",
            Description = "[pt-BR] Testar resposta da API. \n\n " +
                "[en-US] Test API response. ",
            Tags = new[] { "Tests" }
        )]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public string Get()
        {
            return "ok";
        }
    }
}
