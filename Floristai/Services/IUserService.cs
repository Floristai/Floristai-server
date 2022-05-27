
using Floristai.Models;

namespace Floristai.Services
{
    public interface IUserService
    {
        public Task<string> AuthenticateUser(string email, string password);
        public Task<bool> RegisterUser(string email, string password);
        public Task<string> GetUserEmail(int userId);
    }
}