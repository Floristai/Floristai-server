using Floristai.Entities;
using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IUserRepository
    {
        Task<User> InsertUser(User user);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
    }
}