using Floristai.Entities;
using Microsoft.AspNetCore.Authorization;
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
            FlowerEntity flower = new FlowerEntity() { Name = "AA", Id = 1};
            return Ok(flower);
        }

        [HttpGet("secret")]
        [Authorize]//(Policy = "AdministratorOnly")
        public async Task<IActionResult> GetAdminlower()
        {
            FlowerEntity flower = new FlowerEntity() { Name = "Administrator flower", Id = 1 };
            return Ok(flower);
        }
    }
}
