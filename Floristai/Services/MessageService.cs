using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message> CreateMessage(Message message)
        {
            return await _messageRepository.InsertMessage(message);
        }
        public async Task<Message> GetMessage(int id)
        {
            return await _messageRepository.GetMessageById(id);
        }
        public async Task<List<Message>> GetUserMessages(int userId)
        {
            return await _messageRepository.GetMessagesByUser(userId);
        }
        public async Task<List<Message>> GetSenderMessages(int senderId)
        {
            return await _messageRepository.GetMessagesBySender(senderId);
        }
    }
}
