using _01_ShopFaQuery.Contracts.Inventory;
using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{

    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStockStatus(CheckStock stock)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == stock.ProductId);
            var status = new StockStatus();
            if (inventory.CalculateCurrentCount() < stock.Count)
            {

                var productName = _shopContext.Products.Select(product => new { product.Id, product.Name })
                    .FirstOrDefault(x => x.Id == stock.ProductId)?.Name;
                    status.InStock = false;
                    status.ProductName=productName;
                    return status;
            }

            status.InStock = true;
            return status;

        }
    }
}
