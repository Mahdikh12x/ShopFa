namespace InventoryManagement.Application.Contract.Inventory;

public class ReduceInventory:IncreaseInventory
{
    public long OrderId { get; set; }

}