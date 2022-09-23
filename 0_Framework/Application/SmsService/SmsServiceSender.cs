using Microsoft.Extensions.Configuration;
using RestSharp;

namespace _0_Framework.Application.SmsService
{
    public class SmsServiceSender:ISmsServiceSender
    {

        public string LineNumber { get; set; }
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }

        public SmsServiceSender(IConfiguration configuration)
        {
            var _configuration = configuration;
            ApiKey = _configuration.GetSection("SendSmsApi")["apiKey"];
            LineNumber = _configuration.GetSection("SendSmsApi")["lineNumber"];
            BaseUrl = _configuration.GetSection("SendSmsApi")["baseUrl"];
        }

        //public void Send(string number, string message)
        //{
        //    var token = GetToken();
        //    var lines = new SmsLine().GetSmsLines(token);
        //    if (lines == null) return;

        //    var line = lines.SMSLines.Last().LineNumber.ToString();
        //    var data = new MessageSendObject
        //    {
        //        Messages = new List<string>
        //            {message}.ToArray(),
        //        MobileNumbers = new List<string> { number }.ToArray(),
        //        LineNumber = line,
        //        SendDateTime = DateTime.Now,
        //        CanContinueInCaseOfError = true
        //    };
        //    var messageSendResponseObject =
        //        new MessageSend().Send(token, data);

        //    if (messageSendResponseObject.IsSuccessful) return;

        //    line = lines.SMSLines.First().LineNumber.ToString();
        //    data.LineNumber = line;
        //    new MessageSend().Send(token, data);
        //}

        //private string GetToken()
        //{
        //    var smsSecrets = _configuration.GetSection("SmsSecrets");
        //    var tokenService = new Token();
        //    return tokenService.GetToken(smsSecrets["ApiKey"], smsSecrets["SecretKey"]);
        //}

        public void Send(string number, string message)
        {
        }

        public  async Task SendAsync(List<string> numbers, string message)
        {

            var client = new RestClient(BaseUrl);
            var request = new RestRequest
            {
                Method = Method.Post

            };
            request.AddHeader("X-API-KEY", ApiKey);
            request.AddJsonBody(new SendSmsModel
            {
                LineNumber = LineNumber,
                MessageText = message,
                Mobiles = numbers.ToArray()
            }, contentType: "application/json");

            var res = await client.ExecuteAsync(request);
        }
        
    }
}
