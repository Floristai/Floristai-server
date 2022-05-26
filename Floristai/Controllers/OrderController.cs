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
    public class OrderController : ControllerBase
    {
        private readonly IUserIdService _userIdService;
        private readonly IOrderService _orderService;
        public OrderController(IUserIdService userIdService, IOrderService orderService)
        {
            _userIdService = userIdService;
            _orderService = orderService;
        }

        //[HttpGet("secret")]
        //[Authorize(Policy = Policies.AdministratorOnly)]
        //public async Task<IActionResult> GetAdminFlower()
        //{
        //    FlowerEntity flower = new FlowerEntity() { Name = "Administrator flower", FlowerId = 1 };
        //    return Ok(flower);
        //}

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _orderService.getUserOrders(1); //_userIdService.GetUserID()
            return Ok(response);
        } 


    }
}
