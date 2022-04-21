namespace ShopManagement.Application.Contracts.Order;

public class Cart
{
    public double DiscountAmount { get; set; }
    public double TotalItemsAmount { get; set; }
    public double CartPayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> Items { get; set; }

    public Cart()
    {
        Items = new List<CartItem>();
    }
    public void AddToCart(CartItem item)
    {
        Items.Add(item);
        DiscountAmount = item.DiscountAmount + DiscountAmount;
        TotalItemsAmount = item.TotalPrice + TotalItemsAmount;
        CartPayAmount = item.ItemPayAmount + CartPayAmount;
    }

    public void SetPaymentMethod(int paymentMethod)
    {
        PaymentMethod = paymentMethod;
    }
}
