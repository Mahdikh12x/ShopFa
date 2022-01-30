namespace InventoryManagementApplication.Contract.Inventory;

public class ReduceInventory
{
    public long ProductId { get; set; }
    public long Count { get; set; }
    public string Description { get; set; }
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
}