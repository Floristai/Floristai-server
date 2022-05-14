using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;

namespace Floristai.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<User> InsertUser(User user)
        {
            var insertedResult = _dbContext.Users.Add(new UserEntity() { Email = user.Email, Password = user.Password, Type = user.Type});
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(string email, string passwordHash)
        {
            if (!_dbContext.Users.Any(u => u.Email == email && passwordHash == u.Password))
            {
                return null;
            }
            UserEntity userEntity = _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return mapEntityToModel(userEntity);
        }

        private User mapEntityToModel(UserEntity entity)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<UserEntity, User>()
                );
            var mapper = new Mapper(config);
            var user = mapper.Map<User>(entity);
            return user; 
        }
    }
}
