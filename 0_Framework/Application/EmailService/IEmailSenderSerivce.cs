namespace _0_Framework.Application.EmailService
{
public interface IEmailSenderService
    {
        void SendEmail(string title,string body,string destination);
    }
}
