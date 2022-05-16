using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly Mapper _entityToModelMapper;

        public FlowerRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<FlowerEntity, Flower>()
                );
            this._entityToModelMapper = new Mapper(config);
        }

        public async Task<List<Flower>> GetAll()
        {
            var flowers = await _dbContext.Flowers.ToListAsync();
            return _entityToModelMapper.Map<List<FlowerEntity>, List<Flower>>(flowers);
        }
    }
}
