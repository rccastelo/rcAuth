using Microsoft.AspNetCore.Mvc;

namespace rcAuthApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            return "ok";
        }
    }
}
