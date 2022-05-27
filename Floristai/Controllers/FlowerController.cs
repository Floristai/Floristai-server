using Floristai.Entities;
using Floristai.Models;
using Floristai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Security.Claims;

namespace Floristai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;
        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlower()
        {
            FlowerEntity flower = new FlowerEntity() { Name = "AA", FlowerId = 1};
            return Ok(flower);
        }

        [HttpGet("secret")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> GetAdminFlower()
        {
            FlowerEntity flower = new FlowerEntity() { Name = "Administrator flower", FlowerId = 1 };
            return Ok(flower);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        [EnableQuery]
        public async Task<IActionResult> GetFiltered()
        {
            var response = await _flowerService.GetAll();
            return Ok(response);
        }
    }
}
