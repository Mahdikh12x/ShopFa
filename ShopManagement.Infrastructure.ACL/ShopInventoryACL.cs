using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.ACL
{
    public class ShopInventoryAcl:IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public bool ReduceFromInventory(List<OrderItem> orderItems)
        {
            var reduceItems = orderItems
                .Select(x => new ReduceInventory(x.OrderId, x.ProductId, x.Count, "خرید آنلاین")).ToList();

            var result=_inventoryApplication.Reduce(reduceItems);
            return result.IsSuccess;
        }
    }
}