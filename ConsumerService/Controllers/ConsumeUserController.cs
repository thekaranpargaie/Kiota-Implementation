using Microsoft.AspNetCore.Mvc;
using UserServiceSdk;

namespace ConsumerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumeUserController : ControllerBase
    {
        private readonly UserServiceClient _client;

        public ConsumeUserController(UserServiceClient client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _client.Users.GetAsync();
            return Ok(users);
        }
    }
}
