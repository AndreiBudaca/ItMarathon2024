using ItMarathon.Service.HelloWorldService;
using Microsoft.AspNetCore.Mvc;

namespace ItMarathonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private IHelloWorldService helloWorldService;

        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            this.helloWorldService = helloWorldService;
        }

        [HttpGet]
        public async Task<string> HelloWorld(string echo)
        {
            await helloWorldService.AddWordAsync(echo);
            return $"Hello {echo}";
        }
    }
}
