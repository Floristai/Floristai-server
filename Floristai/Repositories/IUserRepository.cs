using Floristai.Entities;
using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IUserRepository
    {
        Task<User> InsertUser(User user);
        Task<User> GetUser(string email, string password);
    }
}