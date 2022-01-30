using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagementApplication.Contract.Inventory;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository;

public class InventoryRepository : BaseRepository<long, Inventory>, IInventoryRepository
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;

    public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
    {
        _inventoryContext = inventoryContext;
        _shopContext = shopContext;
    }

    public Inventory? GetBy(long id)
    {
        return _inventoryContext.Inventory.FirstOrDefault(x => x.Id == id);
    }

    public EditInventory? GetDetails(long id)
    {
        return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
        var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
        {
            Id = x.Id,
            InStock = x.InStock,
            UnitPrice = x.UnitPrice,
            ProductId = x.ProductId,
            CurrentCount = x.CalculateCurrentCount(),
            CreationDate = x.CreationDate.ToFarsi()
        });

        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        if (searchModel.InStock)
            query = query.Where(x => x.InStock == searchModel.InStock);

        var inventory = query.OrderByDescending(x => x.Id).ToList();
        inventory.ForEach(inv => inv.Product = products.FirstOrDefault(x => x.Id == inv.ProductId)?.Name);
        return inventory;
    }

    public List<InventoryOperationsViewModel> GetLog(long inventoryId)
    {
        var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == inventoryId);
        var operations = inventory.Operations.Select(x => new InventoryOperationsViewModel
        {
            Id = x.Id,
            OrderId = x.OrderId,
            Description = x.Description,
            Count = x.Count,
            Operation = x.Operation,
            OperationDate = x.OperationDate.ToFarsi(),
            OperatorId = x.OperatorId,
            Oprator = "مدیر سیستم",
            CurrentCount = x.CurrentCount
        }).OrderByDescending(x => x.Id).ToList();
        return operations;
    }
}