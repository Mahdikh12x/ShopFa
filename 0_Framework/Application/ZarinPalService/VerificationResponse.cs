namespace _0_Framework.Application.ZarinPalService
{
    public class VerificationResponse
    {
        public int Status { get; set; }
        public long RefId { get; set; }

        public VerificationResponse(int status, long refId)
        {
            Status = status;
            RefId = refId;
        }
    }
}
