using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> InsertUser(User user)
        {
            var insertedResult = _dbContext.Users.Add(new UserEntity() { Email = user.Email, Password = user.Password, Type = user.Type});
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(string email, string passwordHash)
        {
            var userExists = await _dbContext.Users.AnyAsync(u => u.Email == email && passwordHash == u.Password);
            if (!userExists)
            {
                return null;
            }
            UserEntity userEntity = await _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return MapEntityToModel(userEntity);
        }

        private User MapEntityToModel(UserEntity entity)
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
