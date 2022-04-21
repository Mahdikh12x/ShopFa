namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public int Count { get; set; }
        public string? Slug { get; set; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount{ get; set; }
        public CartItem()
        {
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = UnitPrice * Count;

        }
    }
}
