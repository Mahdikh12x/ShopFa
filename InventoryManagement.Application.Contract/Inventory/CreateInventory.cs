using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public  class CreateInventory
{
    [Range(1, 100000, ErrorMessage = ValidationMessages.Required)]
    public long ProductId { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.Required)]
    public double UnitePrice { get; set; }
    public List<ProductViewModel> Products { get; set; }
}