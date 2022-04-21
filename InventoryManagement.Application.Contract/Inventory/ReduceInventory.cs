namespace InventoryManagement.Application.Contract.Inventory;

public class ReduceInventory:IncreaseInventory
{
    public long OrderId { get; set; }

    public ReduceInventory()
    {
        
    }
    public ReduceInventory(long orderId,long productId, int count, string? description) : base(productId, count, description)
    {
        OrderId = orderId;
        ProductId=productId;
        Count=count;
        Description=description;
    }
}