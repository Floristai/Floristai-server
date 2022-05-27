using AutoMapper;
using Floristai.Dto;
using Floristai.Models;
using Floristai.Repositories;
using System.Transactions;

namespace Floristai.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlowerRepository _flowerRepository;
        private readonly IFlowerService _flowerService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly Mapper _mapper;

        public OrderService(IOrderRepository orderRepository, IFlowerRepository flowerRepository, IFlowerService flowerService, IUserService userService, IEmailService emailService, Mapper mapper)
        {
            _orderRepository = orderRepository;
            _flowerRepository = flowerRepository;
            _flowerService = flowerService;
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<Order> ConfirmOrder(int orderId)
        {
            var resp = await _orderRepository.UpdateOrderStatus(orderId, OrderStatuses.Confirmed);
            await SendConfirmationEmail(resp);
            return resp;
        }

        public async Task<Order> CompleteOrder(int orderId)
        {
            var resp = await _orderRepository.UpdateOrderStatus(orderId, OrderStatuses.Delivered);
            return resp;
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            var response = await _orderRepository.GetOrders(userId);
            return response.ToList();
        }

        public async Task<Order> InsertNewOrder(OrderInsertDto orderInsertDto, int userId)
        {
            Order order = _mapper.Map<Order>(orderInsertDto);
            var ids = order.OrderLines.Select(x => x.FlowerId).ToList();
            var flowers = await _flowerRepository.GetByIds(ids);
            foreach (Flower flower in flowers)
            {
                var orderLine = order.OrderLines.Single(x => x.FlowerId == flower.FlowerId);
                flower.Quantity -= orderLine.Quantity;
                if (flower.Quantity < 0)
                {
                    throw new InvalidOperationException("Not enough flowers available!");
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _flowerRepository.UpdateFlowers(flowers);
                order.ClientId = userId;
                order.DateCreated = DateTime.Now;
                order.Status = OrderStatuses.Opened;
                return await _orderRepository.InsertOrder(order);
            }
        }

        public async Task SendConfirmationEmail(Order order)
        {
            List<int> flowerIds = new List<int>();
            ICollection<OrderLine> orderLines = order.OrderLines;
            List<Flower> flowers = new List<Flower>();
            foreach (OrderLine line in orderLines)
            {
                flowerIds.Add(line.FlowerId);
            }
            flowers = await _flowerService.GetByIds(flowerIds);
            var flowerEmailQuery =
                from orderLine in orderLines
                join flower in flowers on orderLine.FlowerId equals flower.FlowerId
                select new FlowerEmailData
                {
                    Quantity = orderLine.Quantity,
                    Name = flower.Name,
                    Price = flower.Price
                };
            string userEmail = await _userService.GetUserEmail(order.ClientId);
            var orderEmail = new OrderEmail(flowerEmailQuery.ToList(), userEmail);
            await _emailService.SendEmail(orderEmail);
        }
    }
}
