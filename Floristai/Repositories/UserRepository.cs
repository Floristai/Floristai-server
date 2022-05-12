using Floristai.EFContexts;
using Floristai.Entities;

namespace Floristai.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<DtoUser> InsertUser(string email, string passwordHash)
        {
            var insertedResult = _dbContext.Users.Add(new DtoUser() { Email = email, Password = passwordHash });
            await _dbContext.SaveChangesAsync();
            return insertedResult.Entity;
        }

        public int GetUserId(string email, string passwordHash)
        {
            if (!_dbContext.Users.Any(u => u.Email == email && passwordHash == u.Password))
            {
                return 0;
            }
            DtoUser user = _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return user.Id;
        }

        public async Task<DtoUser> GetUser(string email, string passwordHash)
        {
            if (!_dbContext.Users.Any(u => u.Email == email && passwordHash == u.Password))
            {
                return null;
            }
            DtoUser user = _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return user;
        }
    }
}
