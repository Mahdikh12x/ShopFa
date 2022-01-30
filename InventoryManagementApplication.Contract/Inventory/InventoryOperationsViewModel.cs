
namespace InventoryManagementApplication.Contract.Inventory
{
    public class InventoryOperationsViewModel 
    {
        public long Id { get; set; }
        public long Count { get; set; }
        public bool Operation { get; set; }
        public string OperationDate { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public long OperatorId { get; set; }
        public string Oprator { get; set; }
        public long CurrentCount { get; set; }
      
    }
}
