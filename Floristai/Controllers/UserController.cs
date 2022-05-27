using Floristai.Emails;
using Floristai.Models;
using Floristai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Floristai.Controllers
{
    public class UserLoginData
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly EmailDetails _emailDetails;
        public UserController(IUserService userService, IEmailService emailService, IConfiguration configuration)
        {
            _userService = userService;
            _emailService = emailService;
            _emailDetails = new EmailDetails
            {
                Email = configuration.GetValue<string>("ServiceEmailAccount:Email"),
                Password = configuration.GetValue<string>("ServiceEmailAccount:Password")
            };
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
            var registered = await _userService.RegisterUser(values.Email, values.Password);
            if (!registered)
                return BadRequest("Could not Register");

            var registrationEmail = new RegistrationEmail(values.Email);
            await _emailService.SendEmail(registrationEmail, _emailDetails);
            return Ok();
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var current = await _userService.GetCurrentUser();
            return Ok(current);
        }
        //user gavimas
    }
}
