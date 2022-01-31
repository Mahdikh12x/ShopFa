using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
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
            var inventory = new Inventory(command.ProductId, command.UnitePrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            inventory.Edit(command.ProductId, command.UnitePrice);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null) 
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            const long operatorId = 1;
            inventory.Increase(command.Count,operatorId,command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            const long operatorId = 1;
            inventory.Reduce(command.Count,command.OperatorId,command.Description,command.OrderId);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation= new OperationResult();
            foreach (var inventory in command)
            {
               var reduce= _inventoryRepository.Get(inventory.InventoryId);
                reduce.Reduce(inventory.Count,inventory.OperatorId,inventory.Description,inventory.OrderId);
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

        public List<InventoryOperationViewModel>? GetOperations(long inventoryId)
        {
            return _inventoryRepository.GetOperations(inventoryId);
        }
    }
}