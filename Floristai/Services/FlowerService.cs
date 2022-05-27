using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerService(IFlowerRepository flowerRepository, IJwtKeyHoldingService jwtKeyHoldingService)
        {
            _flowerRepository = flowerRepository;
        }

        public async Task<bool> DeleteFlower(int flowerId)
        {
            return await _flowerRepository.DeleteFlower(flowerId);
        }

        public async Task<List<Flower>> GetAll()
        {
            return await _flowerRepository.GetAll();
        }

        public async Task<Flower> InsertFlower(Flower flower)
        {
            return await _flowerRepository.InsertFlower(flower);
        }

        public async Task<Flower> UpdateFlower(Flower flower)
        {
            List<Flower> flowers = await _flowerRepository.UpdateFlowers(new List<Flower>() { flower });
            return flowers[0];
        }
    }
}
