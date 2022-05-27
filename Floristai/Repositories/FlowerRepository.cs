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

        public async Task<bool> DeleteFlower(int flowerId)
        {
            var flowerEntity = new FlowerEntity { FlowerId = flowerId };
            _dbContext.Flowers.Attach(flowerEntity);
            _dbContext.Flowers.Remove(flowerEntity);
            await _dbContext.SaveChangesAsync();
            return true;
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

        public async Task<Flower> InsertFlower(Flower flower)
        {
            _dbContext.Flowers.Add(_mapper.Map<FlowerEntity>(flower));
            await _dbContext.SaveChangesAsync();
            return flower;
        }

        public async Task<List<Flower>> UpdateFlowers(List<Flower> flowers)
        {
            List<FlowerEntity> flowerEntities = _mapper.Map<List<Flower>, List<FlowerEntity>>(flowers);
            flowerEntities.ForEach(flower => _dbContext.Update(flower));
            await _dbContext.SaveChangesAsync();
            return flowers;
        }
    }
}
