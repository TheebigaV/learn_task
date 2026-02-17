using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello, World!";
        }
    }
}
