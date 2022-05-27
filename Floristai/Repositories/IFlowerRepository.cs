using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IFlowerRepository
    {
        Task<List<Flower>> GetAll();
        Task<List<Flower>> GetByIds(List<int> ids);
        Task<List<Flower>> UpdateFlowers(List<Flower> flowers);
    }
}