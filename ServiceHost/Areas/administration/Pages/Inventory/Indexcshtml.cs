using InventoryManagementApplication.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.administration.Pages.Inventory;

public class IndexModel : PageModel
{
    public List<InventoryViewModel>Inventories;
    public InventorySearchModel SearchModel;
    public SelectList Products;
    private readonly IProductApplication _productApplication;
    private readonly IInventoryApplication _inventoryApplication;
    public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
    {
        _productApplication = productApplication;
        _inventoryApplication = inventoryApplication;
    }

    public void OnGet(InventorySearchModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        Inventories = _inventoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new CreateInventory
        {
            Products = _productApplication.GetProducts()
        };
        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(CreateInventory command)
    {
        var result = _inventoryApplication.Create(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var inventory = _inventoryApplication.GetDetails(id);
        if (inventory != null)
            inventory.Products = _productApplication.GetProducts();

        return Partial("./Edit", inventory);
    }
    public JsonResult OnPostEdit(EditInventory command)
    {
        var result = _inventoryApplication.Edit(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetIncrease(long id)
    {
        var command = new IncreaseInventory
        {
            InventoryId = id
        };

        return Partial("Increase", command);
    }

    public JsonResult OnPostIncrease(IncreaseInventory command)
    {
        var result = _inventoryApplication.Increase(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetReduce(long id)
    {
        var command = new ReduceInventory
        {
            InventoryId = id
        };

        return Partial("Reduce", command);
    }

    public JsonResult OnPostReduce(ReduceInventory command)
    {
        var result = _inventoryApplication.Reduce(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetLog(long id)
    {
        var data = _inventoryApplication.GetLog(id);
        return Partial("OperationLog", data);
    }
}
