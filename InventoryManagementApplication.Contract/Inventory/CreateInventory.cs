using ShopManagement.Application.Contracts.Product;

namespace InventoryManagementApplication.Contract.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}

