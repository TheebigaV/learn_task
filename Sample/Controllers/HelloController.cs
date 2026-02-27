using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello, World!";
        }
    }
}
