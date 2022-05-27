using Floristai.Models;

using Floristai.Emails;

namespace Floristai.Services
{
    public interface IEmailService
    {
        Task SendEmail(IEmail email, EmailDetails emailDetails);
    }
}