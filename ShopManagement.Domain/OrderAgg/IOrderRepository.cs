using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg;

public interface IOrderRepository:IRepository<long,Order>
{
    double GetPayAmountBy(long orderId);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
    List<OrderItemViewModel> GetItems(long id);
}