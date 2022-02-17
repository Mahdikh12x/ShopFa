using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitePrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation>? Operations { get; private set; }
        public Inventory(long productId,double unitePrice)
        {
            ProductId = productId;
            UnitePrice = unitePrice;
            InStock = false;
        }
        public void Edit(long productId,double unitePrice)
        {
            ProductId = productId;
            UnitePrice = unitePrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, operatorId, 0, description, count, currentCount, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(long count, long operatorId, string description, long orderId)
        {
            var currentCount=CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, operatorId, orderId, description, count, currentCount, Id);
            Operations.Add(operation);
            InStock= currentCount > 0;
        }
    }
}
