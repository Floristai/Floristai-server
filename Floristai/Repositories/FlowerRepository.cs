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
        private readonly Mapper _mapper;

        public FlowerRepository(DatabaseContext dbContext, Mapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<Flower>> GetAll()
        {
            var flowers = await _dbContext.Flowers.ToListAsync();
            return _mapper.Map<List<FlowerEntity>, List<Flower>>(flowers);
        }

        public async Task<List<Flower>> GetByIds(List<int> ids)
        {
            var flowers = await _dbContext.Flowers.Where(x => ids.Contains(x.FlowerId)).AsNoTracking().ToListAsync();
            return _mapper.Map<List<FlowerEntity>, List<Flower>>(flowers);
        }

        public async Task<List<Flower>> UpdateFlowers(List<Flower> flowers)
        {
            flowers.ForEach(x => _dbContext.Flowers.Attach(_mapper.Map<FlowerEntity>(x)));
            await _dbContext.SaveChangesAsync();
            return flowers;
        }
    }
}
