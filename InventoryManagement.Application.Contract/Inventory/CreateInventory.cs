using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public  class CreateInventory
{
    public long ProductId { get; set; }
    public double UnitePrice { get; set; }
    public List<ProductViewModel> Products { get; set; }
}