using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            return Ok(new[]
            {
                new UserDto { Id = 1, Name = "Alice" },
                new UserDto { Id = 2, Name = "Bob" }
            });
        }
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
