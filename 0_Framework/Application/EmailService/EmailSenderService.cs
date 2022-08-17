using MailKit.Net.Smtp;
using MimeKit;
namespace _0_Framework.Application.EmailService
{
    public class EmailSenderService:IEmailSenderService
    {
        public void SendEmail(string title, string body, string destination)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Mahdi", "mahdikhodadadi12x@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>{body}</h1>",
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("111.11.111.111", 25, false);
            client.Authenticate("mahdikhodadadi12x@gmail.com", "");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
