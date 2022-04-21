using System.Text.Json;
using Dto.Payment;
using Microsoft.Extensions.Configuration;
using RestSharp;
using ZarinPal.Class;

namespace _0_Framework.Application.ZarinPalService
{
    public class ZarinPalFactory:IZarinPalFactory
    {
        public string Prefix { get; set; }
        public string MerchantId { get; set; }
        public string CallBackURL{ get; set; }

        public ZarinPalFactory(IConfiguration configuration)
        {
            MerchantId = configuration.GetSection("payment")["merchant"];
            Prefix = configuration.GetSection("payment")["method"];
            CallBackURL = configuration.GetSection("payment")["callbackurl"];
        }

        public async Task<PaymentResponse> CreatePaymentRequest(string amount, string mobile, string email, string description, long orderId)
        {

            amount = amount.Replace(",", "");
            Payment pay = new();
            var result = await pay.Request(new DtoRequest
            {
                Mobile =mobile,
                CallbackUrl = $"{CallBackURL}&oId={orderId}",
                Description = description,
                Email = email,
                Amount = int.Parse(amount),
                MerchantId = MerchantId
            }, Payment.Mode.sandbox);

            var response = new PaymentResponse(result.Status, result.Authority,Prefix);
            return response;

        }

        public async Task<VerificationResponse> CreateVerificationRequest(string authority, string price)
        {
            Payment payment = new();
            var verification = await payment.Verification(new DtoVerification
            {
                Authority = authority,
                Amount = int.Parse(price),
                MerchantId = MerchantId
            }, Payment.Mode.sandbox);

            var response = new VerificationResponse(verification.Status, verification.RefId);
            return response;
        }
    }
}
