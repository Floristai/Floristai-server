using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IFlowerRepository
    {
        Task<List<Flower>> GetAll();
    }
}