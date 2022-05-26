using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly Mapper _entityToModelMapper;

        public OrderRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<OrderEntity, Order>()
                );
            this._entityToModelMapper = new Mapper(config);
        }

        public async Task<Order> InsertOrder(Order order)
        {
            var insertedResult = _dbContext.Orders.Add(MapModelToEntity(order));
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders(int userId)
        {
            List<OrderEntity> orderEntities = await _dbContext.Orders.Where(x => x.ClientId == userId).Include(x => x.OrderLines).ToListAsync();
            var toRet = _entityToModelMapper.Map<List<OrderEntity>, List<Order>>(orderEntities);
            return toRet;
        }

        private OrderEntity MapModelToEntity(Order entity)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Order, OrderEntity>()
                );
            var mapper = new Mapper(config);
            var orderEntity = mapper.Map<OrderEntity>(entity);
            return orderEntity;
        }
    }
}
