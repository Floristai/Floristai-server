using Floristai.Models;

namespace Floristai.Services
{
    public interface IEmailService
    {
        Task SendEmail(IEmail email, EmailDetails emailDetails);
    }
}