using Floristai.Models;

namespace Floristai.Repositories
{
    public interface ILoggingRepository
    {
        Task<Logging> InsertLogging(Logging logging);
    }
}