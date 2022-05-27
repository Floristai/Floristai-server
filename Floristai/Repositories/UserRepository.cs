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
        private readonly Mapper _mapper;

        public UserRepository(DatabaseContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> InsertUser(User user)
        {
            var insertedResult = _dbContext.Users.Add(new UserEntity() { Email = user.Email, Password = user.Password, Type = user.Type});
            await _dbContext.SaveChangesAsync();
            return user;
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

        public async Task<int> GetUserId(string email)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return response.UserId;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string passwordHash)
        {
            var userExists = await _dbContext.Users.AnyAsync(u => u.Email == email && passwordHash == u.Password);
            if (!userExists)
            {
                return null;
            }
            UserEntity userEntity = await _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return _mapper.Map<User>(userEntity);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId == id);
            return _mapper.Map<User>(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<User>(user);
        }
    }
}
