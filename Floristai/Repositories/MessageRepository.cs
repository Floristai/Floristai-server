using AutoMapper;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristai.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly Mapper _entityToModelMapper;

        public MessageRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<MessageEntity, Message>()
                );
            _entityToModelMapper = new Mapper(config);
        }

        public async Task<Message> InsertMessage(Message message)
        {
            var insertedMessage = _dbContext.Messages.Add(new MessageEntity { UserId = message.UserId, SenderId = message.SenderId, Content = message.Content });
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<Message> GetMessageById(int id)
        {
            var message = _dbContext.Messages.FindAsync(id);
            return _entityToModelMapper.Map<Message>(message);
        }

        public async Task<List<Message>> GetMessagesByUser(int userId)
        {
            var messages = _dbContext.Messages.Where(message => message.UserId == userId).ToList();
            return _entityToModelMapper.Map<List<MessageEntity>, List<Message>>(messages);
        }

        public async Task<List<Message>> GetMessagesBySender(int senderId)
        {
            var messages = _dbContext.Messages.Where(message => message.SenderId == senderId).ToList();
            return _entityToModelMapper.Map<List<MessageEntity>, List<Message>>(messages);
        }
    }
}
