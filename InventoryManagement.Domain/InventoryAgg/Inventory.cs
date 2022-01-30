using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public bool InStock { get; private set; }
        public double UnitPrice { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }    
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock=false;

        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count,long operatorId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, 0, description, operatorId, currentCount, Id);
            Operations.Add(operation);
            InStock = currentCount>0;
        }

        public void Reduce(long count, long operatorId, long orderId, string description)
        {
            var currentCount=CalculateCurrentCount()- count;
            var operation = new InventoryOperation(false, count, orderId, description, operatorId, currentCount, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }

    }
}

