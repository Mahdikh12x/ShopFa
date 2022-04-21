namespace ShopManagement.Application.Contracts.Order;

public interface IOrderApplication
{
    void Cancel(long id);
    void Confirm(long id);
    long PlaceOrder(Cart cart);
    double GetPayAmountBy(long orderId);
    List<OrderItemViewModel>GetItems(long id);
    string PaymentSucceeded(long orderId, long refId);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
}