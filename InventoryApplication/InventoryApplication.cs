using _0_Framework.Application;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagementApplication.Contract.Inventory;

namespace InventoryManagementApplication;

public class InventoryApplication : IInventoryApplication
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryApplication(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public OperationResult Create(CreateInventory command)
    {
        var operation = new OperationResult();
        if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            return operation.Failed(ApplicationValidationMessages.Duplicated);
        var inventory = new Inventory(command.ProductId, command.UnitPrice);
        _inventoryRepository.Create(inventory);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.GetBy(command.Id);
        if (inventory == null)
            return operation.Failed(ApplicationValidationMessages.NotExisted);
        if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            return operation.Failed(ApplicationValidationMessages.Duplicated);
        inventory.Edit(command.ProductId, command.UnitPrice);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Increase(IncreaseInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory == null)
            return operation.Failed(ApplicationValidationMessages.Duplicated);
        const long operatorId = 1;
        inventory.Increase(command.Count, operatorId, command.Description);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Reduce(ReduceInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory == null)
            return operation.Failed(ApplicationValidationMessages.Duplicated);
        const long operatorId = 1;
        inventory.Reduce(command.Count, operatorId, command.OrderId, command.Description);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Reduce(List<ReduceInventory> command)
    {
        const long operatorId = 1;

        var operation = new OperationResult();
        foreach (var item in command)
        {
            var inventory = _inventoryRepository.Get(item.InventoryId);
            inventory.Reduce(item.Count, operatorId, item.OrderId, item.Description);

        }
        _inventoryRepository.SaveChanges();
      return operation.Succedded();

    }

    public EditInventory? GetDetails(long id)
    {
        return _inventoryRepository.GetDetails(id);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        return _inventoryRepository.Search(searchModel);
    }

    public List<InventoryOperationsViewModel> GetLog(long inventoryId)
    {
        return _inventoryRepository.GetLog(inventoryId);
    }
}