namespace Floristai.Services
{
    public class RegistrationEmail : IEmail
    {
        string body = "Thank you for registering an account on Floristai website.";
        string subject = "Registration to Floristai";
        string recipientEmail = "farmergaren@gmail.com";
        public RegistrationEmail(string userEmail)
        {
            recipientEmail = userEmail;
        }
        public string GetBody()
        {
            return this.body;
        }
        public string GetSubject()
        {
            return this.subject;
        }
        public string GetRecipientEmail()
        {
            return this.recipientEmail;
        }
    }
}
