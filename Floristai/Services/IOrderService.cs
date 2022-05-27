using Floristai.Dto;
using Floristai.Models;

namespace Floristai.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetUserOrders(int userId);
        Task<Order> InsertNewOrder(OrderInsertDto orderInsertDto, int userId);
        Task<Order> ConfirmOrder(int orderId);
        Task<Order> CompleteOrder(int orderId);
    }
}