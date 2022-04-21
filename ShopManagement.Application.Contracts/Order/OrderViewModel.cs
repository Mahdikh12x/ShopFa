namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string? AccountName { get; set; }
        public int DiscountRate { get; set; }
        public string? DiscountAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TotalAmount { get; set; }
        public string? PayAmount { get; set; }
        public bool IsPayed { get; set; }
        public bool IsCanceled { get; set; }
        public string? IssuesTrackingNum { get; set; }
        public string? CreationDate { get; set; }
        public long RefId { get; set; }
    }
}
