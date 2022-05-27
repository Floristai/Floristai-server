using Floristai.Dto;
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
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] OrderInsertDto orderDto )
        {
            var result = await _orderService.InsertNewOrder(orderDto, _userService.GetCurrentUserId());  
            try
            { 
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _orderService.GetUserOrders(_userService.GetCurrentUserId());
            return Ok(response);
        }

        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpPut("{orderId}/confirm")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] int orderId)
        {
            var response = await _orderService.ConfirmOrder(orderId);
            return Ok(response);
        }
        
        [Authorize(Policy = Policies.AdministratorOnly)]
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder([FromRoute] int orderId)
        {
            var response = await _orderService.CompleteOrder(orderId);
            return Ok(response);
        }
    }
}
