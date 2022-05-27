namespace Floristai.Services
{
    public interface IEmail
    {
        string GetBody();
        string GetSubject();
        string GetRecipientEmail();
    }
}
