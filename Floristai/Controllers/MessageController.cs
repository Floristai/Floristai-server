using Floristai.Models;
using Floristai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            var _message = await _messageService.CreateMessage(message);
            return Ok(_message);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByUser([FromQuery] int userId)
        {
            var messages = await _messageService.GetUserMessages(userId);
            return Ok(messages);
        }

        [HttpGet("sender")]
        [Authorize]
        public async Task<IActionResult> GetMessagesBySender([FromQuery] int senderId)
        {
            var messages = await _messageService.GetSenderMessages(senderId);
            return Ok(messages);
        }
    }
}
