using Microsoft.AspNetCore.Mvc;

namespace ItMarathonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet(Name = "HelloWorld")]
        public string HelloWorld(string echo)
        {
            return $"Hello {echo}";
        }
    }
}
