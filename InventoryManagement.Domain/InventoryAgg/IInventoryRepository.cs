using _0_Framework.Domain;
using InventoryManagementApplication.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg;

public interface IInventoryRepository : IRepository<long, Inventory>
{
    Inventory? GetBy(long id);
    EditInventory? GetDetails(long id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationsViewModel> GetLog(long inventoryId);
}