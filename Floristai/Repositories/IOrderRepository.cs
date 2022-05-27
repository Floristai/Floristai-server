using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders(int userId);
        Task<Order> InsertOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> UpdateOrderStatus(int orderId, string status);
    }
}