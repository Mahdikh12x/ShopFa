namespace InventoryManagement.Domain.InventoryAgg;

public class InventoryOperation
{
    public InventoryOperation(bool operation, long operatorId, long orderId, string description, long count, long currentCount, long inventoryId)
    {
        Operation = operation;
        OperatorId = operatorId;
        OrderId = orderId;
        Description = description;
        Count = count;
        CurrentCount = currentCount;
        InventoryId = inventoryId;
        OperationDate=DateTime.Now;
    }

    public long Id { get; private set; }
    public bool Operation { get; private set; }
    public long OperatorId { get; private set; }
    public long OrderId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public string Description { get; private set; }
    public long Count { get; private set; }
    public long CurrentCount { get; private set; }
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }
}