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
        private readonly IUserIdService _userIdService;
        private readonly IOrderService _orderService;
        public OrderController(IUserIdService userIdService, IOrderService orderService)
        {
            _userIdService = userIdService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInsertDto orderDto )
        {
            var result = await _orderService.InsertNewOrder(orderDto, 1);  //_userIdService.GetUserID()
            try
            { 
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _orderService.GetUserOrders(1); //_userIdService.GetUserID()
            return Ok(response);
        }

        [HttpPut("order/{orderId}/confirm")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] int orderId)
        {
            var response = await _orderService.ConfirmOrder(orderId);
            return Ok(response);
        }
        
        [HttpPut("order/{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder([FromRoute] int orderId)
        {
            var response = await _orderService.CompleteOrder(orderId);
            return Ok(response);
        }


    }
}
