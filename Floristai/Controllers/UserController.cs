using Floristai.Middleware;
using Floristai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristai.Controllers
{
    public class UserLoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase{ 
        private readonly IUserService _userService;
        public UserController(IUserService userManager)
        {
            _userService = userManager;
        }

        [HttpPost("login")]
        [Logging(LoggingEvent.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> AttemptLogin([FromBody] UserLoginData values)
        {
            var token = await _userService.AuthenticateUser(values.Email, values.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> AttemptRegister([FromBody] UserLoginData values)
        {
            var registered = await _userService.RegisterUser(values.Email, values.Password);
            if (registered)
                return Ok();
            return BadRequest("Could not Register");
        }

    }
}
