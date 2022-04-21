using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IAuthHelper _authHelper;
        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
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
            var operatorId = _authHelper.GetAccountId();
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
            var operatorId = _authHelper.GetAccountId();
            inventory.Reduce(command.Count,operatorId,command.Description,command.OrderId);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operatorId = _authHelper.GetAccountId();
            var operation= new OperationResult();
            foreach (var inventory in command)
            {
               var reduce= _inventoryRepository.GetBy(inventory.ProductId);
                reduce.Reduce(inventory.Count,operatorId,inventory.Description,inventory.OrderId);
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