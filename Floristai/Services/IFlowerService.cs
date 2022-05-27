using Floristai.Models;

namespace Floristai.Services
{
    public interface IFlowerService
    {
        Task<List<Flower>> GetAll();

        Task<Flower> InsertFlower(Flower flower);
        Task<Flower> UpdateFlower(Flower flower);
        Task<bool> DeleteFlower(int flowerId);
    }
}