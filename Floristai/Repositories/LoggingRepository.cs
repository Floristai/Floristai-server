using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly DatabaseContext _dbContext;

        public LoggingRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Logging> InsertLogging(Logging logging)
        {
            var insertedResult = _dbContext.Logging.Add(new LoggingEntity() { User = logging.User, Permissions = logging.Permissions, Time = logging.Time, Method = logging.Method });
            await _dbContext.SaveChangesAsync();
            return logging;
        }

    }
}
