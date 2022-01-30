using _0_Framework.Application;

namespace InventoryManagementApplication.Contract.Inventory;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);
    OperationResult Edit(EditInventory command);
    OperationResult Increase(IncreaseInventory command);
    OperationResult Reduce(ReduceInventory command);
    OperationResult Reduce(List<ReduceInventory> command);
    EditInventory? GetDetails(long id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationsViewModel> GetLog(long inventoryId);
}