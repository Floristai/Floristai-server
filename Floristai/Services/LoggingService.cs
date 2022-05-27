using Floristai.Entities;
using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        public LoggingService(ILoggingRepository loggingRepository, IJwtKeyHoldingService jwtKeyHoldingService)
        {
            _loggingRepository = loggingRepository;
        }

        public async Task<bool> AddNewLogging(string user, string permissions, string method)
        {

            Logging toInsert = new Logging { User = user, Permissions = permissions, Time = DateTime.Now.ToString(), Method = method }; 
            await _loggingRepository.InsertLogging(toInsert);
            return true;

        }
    }
}
