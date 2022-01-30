namespace InventoryManagement.Domain.InventoryAgg;

public class InventoryOperation
{
    public long Id { get; private set; }
    public long Count { get; private set; }
    public bool Operation { get; private set; }
    public DateTime OperationDate { get; private set; } 
    public long OrderId { get; private set; }
    public string Description { get; private set; }
    public long OperatorId { get; private set; }
    public long CurrentCount { get; private set; }
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }
    public InventoryOperation(bool operation, long count, long orderId, string description, long operatorId, long currentCount, long inventoryId)
    {
        Operation = operation;
        Count = count;
        OrderId = orderId;
        Description = description;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        InventoryId = inventoryId;
        OperationDate=DateTime.Now;
    }
}