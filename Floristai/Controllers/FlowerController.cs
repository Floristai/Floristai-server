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
        [AllowAnonymous]
        [EnableQuery]
        public async Task<IActionResult> GetFiltered()
        {
            var response = await _flowerService.GetAll();
            return Ok(response);
        }

        //CRUD adminui

        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Flower flower)
        {
            return Ok(flower);
        }
        
        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Flower flower)
        {
            return Ok(flower);
        }
        
        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int flowerId)
        {
            return Ok();
        }
    }
}
