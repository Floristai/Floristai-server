using Floristai.Models;

namespace Floristai.Services
{
    public interface IOrderService
    {
        Task<List<Order>> getUserOrders(int userId);
    }
}