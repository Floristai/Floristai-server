namespace Floristai.Emails
{
    public interface IEmail
    {
        string GetBody();
        string GetSubject();
        string GetRecipientEmail();
    }
}
