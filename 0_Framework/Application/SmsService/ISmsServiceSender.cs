namespace _0_Framework.Application.SmsService
{
    public interface ISmsServiceSender
    {
        void Send(string number, string message);
        Task SendAsync(List<string> numbers, string message);
    }
}
