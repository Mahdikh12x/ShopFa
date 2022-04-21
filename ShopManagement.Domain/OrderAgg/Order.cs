using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get; private set; }
        public double DiscountAmount { get; private set; }
        public int PaymentMethod { get; private set; }
        public double TotalAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPayed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string? IssuesTrackingNum { get; private set; }
        public long RefId { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double discountAmount, int paymentMethod, double totalAmount, double payAmount)
        {
            AccountId = accountId;
            DiscountAmount = discountAmount;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;
            PayAmount = payAmount;
            IsPayed = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refId)
        {
            IsPayed = true;
            IsCanceled = false;
            if (refId != 0)
                RefId = refId;
        }

        public void SetIssueTrackingNumber(string num)
        {
            IssuesTrackingNum = num;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem orderItem)
        {
            Items.Add(orderItem);
        }
    }
}
