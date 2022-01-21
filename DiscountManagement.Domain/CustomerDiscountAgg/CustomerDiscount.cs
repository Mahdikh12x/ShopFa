using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long ProductId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }
        public int DiscountRate { get; private set; }
    
        public CustomerDiscount(long productId, DateTime startDate, DateTime endDate, string reason, int discountRate)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            DiscountRate = discountRate;
        }
        public void Edit(long productId, DateTime startDate, DateTime endDate, string reason, int discountRate)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            DiscountRate = discountRate;
        }
    }
}
