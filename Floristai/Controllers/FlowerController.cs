using Floristai.Entities;
using Floristai.Models;
using Floristai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
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
            var response = await _flowerService.InsertFlower(flower);
            return Ok(response);
        }
        

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Flower flower)
        {
            try
            {
                var response = await _flowerService.UpdateFlower(flower);
                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
        }
        
        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int flowerId)
        {
            bool succeeded = await _flowerService.DeleteFlower(flowerId);
            if (succeeded)
                return Ok();
            return NotFound();
        }
    }
}
