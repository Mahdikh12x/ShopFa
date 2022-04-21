using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository;

public class InventoryRepository:BaseRepository<long,Inventory>,IInventoryRepository
{
    private readonly InventoryContext _context;
    private readonly ShopContext _shopContext;
    private readonly AccountContext _accountContext;
    public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
    {
        _context=context;
        _shopContext = shopContext;
        _accountContext = accountContext;
    }

    public EditInventory? GetDetails(long id)
    {
        return _context.Inventory.Select(x => new EditInventory
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UnitePrice = x.UnitePrice,
        }).FirstOrDefault(x => x.Id == id);
    }

    public Inventory? GetBy(long productId)
    {
        return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
        var query = _context.Inventory.Select(x => new InventoryViewModel
        {
            Id = x.Id,
            UnitePrice = x.UnitePrice,
            CreationDate = x.CreationDate.ToFarsi(),
            CurrentCount = x.CalculateCurrentCount(),
            ProductId = x.ProductId,
            InStock = x.InStock
        });

        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);

        if (searchModel.InStock)
            query = query.Where(x => x.InStock != searchModel.InStock);

        var inventories = query.OrderByDescending(x => x.Id).ToList();
        
        inventories.ForEach(inv=>inv.Product=products.FirstOrDefault(x=>x.Id==inv.ProductId)?.Name);
        return inventories;
    }

    public List<InventoryOperationViewModel>? GetOperations(long inventoryId)
    {
        var accounts = _accountContext.Users.Select(x => new { x.Id, x.Fullname }).ToList();
        var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
        var operations = inventory?.Operations.Select(x => new InventoryOperationViewModel
            {
            Operation = x.Operation,
            Count = x.Count,
            CurrentCount = x.CurrentCount,
            Description = x.Description,
            Id = x.Id,
            OperationDate = x.OperationDate.ToFarsi(),
            OrderId = x.OrderId,
            OperatorId = x.OperatorId
            })
            .OrderByDescending(x => x.Id).ToList();

        operations?.ForEach(user=>user.Operator=accounts.FirstOrDefault(x=>x.Id==user.OperatorId)?.Fullname);


        return operations;
    }
}