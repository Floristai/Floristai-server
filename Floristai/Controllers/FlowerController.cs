using Floristai.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Floristai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFlower()
        {
            DtoFlower flower = new DtoFlower() { Name = "AA", Id = 1};
            return Ok(flower);
        }
    }
}
