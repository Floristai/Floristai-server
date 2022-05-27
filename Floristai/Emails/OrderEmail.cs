using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Emails
{
    public class OrderEmail : IEmail
    {
        private readonly IUserRepository _userRepository;
        string body = "Thank you for ordering flowers from Floristai. Your order was received and is on the way to be processed.\n";
        string subject = "Your order from Floristai";
        string recipientEmail = "farmergaren@gmail.com";
        float subtotal = 0;
        public OrderEmail(List<FlowerEmailData> flowerEmailDatas, string userEmail)
        {
            body += "Your order is:\n";
            foreach (FlowerEmailData flower in flowerEmailDatas)
            {
                body += flower.Name + " amount: " + flower.Quantity + " price: " + flower.Price + "€" + "\n";
                subtotal += flower.Price;
            }
            body += "Total:" + subtotal + "€" + "\n";
            body += "\nYou will receive details about the expected delivery in a following email.";

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
