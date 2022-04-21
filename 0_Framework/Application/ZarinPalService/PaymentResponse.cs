namespace _0_Framework.Application.ZarinPalService
{
    public class PaymentResponse
    {
        public int Status { get; set; }
        public string Authority { get; set; }
        public string Prefix { get; set; }
        public PaymentResponse(int status, string authority,string prefix)
        {
            Status = status;
            Authority = authority;
            Prefix=prefix;
        }
    }
}
