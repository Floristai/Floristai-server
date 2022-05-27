using AutoMapper;
using AutoMapper.QueryableExtensions;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly Mapper _mapper;

        public OrderRepository(DatabaseContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Order> InsertOrder(Order order)
        {
            var insertedResult = _dbContext.Orders.Add(_mapper.Map<OrderEntity>(order));
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders(int userId)
        {
            List<OrderEntity> orderEntities = await _dbContext.Orders.Where(x => x.ClientId == userId).Include(x => x.OrderLines).ToListAsync();
            //var toRet = _entityToModelMapper.Map<List<OrderEntity>, List<Order>>(orderEntities);
            var x = orderEntities.AsQueryable().ProjectTo<Order>(_mapper.ConfigurationProvider);
            return x.ToList();
        }

        public async Task<Order> getOrderById(int id)
        {
            OrderEntity orderEntity = await _dbContext.Orders.SingleAsync(x => x.OrderId == id);
            _dbContext.Entry(orderEntity).State = EntityState.Detached;
            return _mapper.Map<Order>(orderEntity);
        }

        public async Task<Order> UpdateOrderStatus(int orderId, string status)
        {

            OrderEntity order = await _dbContext.Orders.SingleAsync(x => x.OrderId == orderId);
            order.Status = status;
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Order>(order);
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
