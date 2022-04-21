namespace InventoryManagement.Application.Contract.Inventory;

public class IncreaseInventory
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public string? Description { get; set; }

    public IncreaseInventory()
    {
        
    }
    public IncreaseInventory(long productId, int count, string? description)
    {
        ProductId = productId;
        Count = count;
        Description = description;
    }
}