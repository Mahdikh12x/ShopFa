namespace _0_Framework.Application.ZarinPalService
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string IssueTrackingNo { get; set; }

        public PaymentResult Cash(string issueTrackingNum)
        {
            IssueTrackingNo = issueTrackingNum;
            return this;
        }
        public PaymentResult Succeeded(string message)
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }

        public PaymentResult Failed(string message)
        {
            Message = message;
            IsSuccessful = false;
            return this;
        }
    }
}
