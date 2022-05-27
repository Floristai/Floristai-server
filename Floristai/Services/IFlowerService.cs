using Floristai.Models;

namespace Floristai.Services
{
    public interface IFlowerService
    {
        Task<List<Flower>> GetAll();
        Task<List<Flower>> GetByIds(List<int> ids);
    }
}