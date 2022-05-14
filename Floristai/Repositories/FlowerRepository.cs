using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;

namespace Floristai.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly DatabaseContext _dbContext;
        private Mapper entityToModelMapper;
        public FlowerRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<FlowerEntity, Flower>()
                );
            this.entityToModelMapper = new Mapper(config);
        }

        public async Task<List<Flower>> getAll()
        {
            return entityToModelMapper.Map<List<FlowerEntity>, List<Flower>>(_dbContext.Flowers.ToList());
        }
    }
}
