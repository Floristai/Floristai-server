using Floristai.Models;

namespace Floristai.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessage(Message message);
        Task<Message> GetMessageById(int id);
        Task<List<Message>> GetMessagesByUser(int userId);
        Task<List<Message>> GetMessagesBySender(int senderId);
    }
}
