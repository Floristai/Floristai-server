using Floristai.Models;

namespace Floristai.Services
{
    public interface IMessageService
    {
        Task<Message> CreateMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<List<Message>> GetUserMessages(int userId);
        Task<List<Message>> GetSenderMessages(int senderId);
    }
}
