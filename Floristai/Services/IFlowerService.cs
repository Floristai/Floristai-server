using Floristai.Models;

namespace Floristai.Services
{
    public interface IFlowerService
    {
        Task<List<Flower>> getAll();
    }
}