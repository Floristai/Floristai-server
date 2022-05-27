using Floristai.Models;

namespace Floristai.Services
{
    public interface ILoggingService
    {
        Task<bool> AddNewLogging(string user, string permissions, string method);
    }
}