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
        private readonly IEmailService _emailService;
        public UserController(IUserService userManager, IEmailService emailService)
        {
            _userService = userManager;
            _emailService = emailService;
        }

        [HttpPost("login")]
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
            var registrationEmail = new RegistrationEmail(values.Email);
            await _emailService.SendEmail(registrationEmail);
            var registered = await _userService.RegisterUser(values.Email, values.Password);
            if (registered)
                return Ok();
            return BadRequest("Could not Register");
        }

    }
}
