using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> getUserOrders(int userId)
        {
            var response = await _orderRepository.GetOrders(userId);
            return response.ToList();
        }

        //public Task<Order> CreateOrder(Order )
        //{
        //    return null;
        //}

        //public Task<Order> ConfirmOrder()
        //{
        //    IEmailBody emailBody = new ConfirmationEmail();
        //    EmailSender.sendEmail(emailBody);
        //    return null;
        //}

        //public Task<Order> CompleteOrder()
        //{
        //    return null;
        //}
    }
}
