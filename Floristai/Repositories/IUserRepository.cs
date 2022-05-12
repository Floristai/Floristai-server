using Floristai.Entities;

namespace Floristai.Repositories
{
    public interface IUserRepository
    {
        int GetUserId(string email, string password);
        Task<DtoUser> InsertUser(string email, string password);
        Task<DtoUser> GetUser(string email, string password);
    }
}