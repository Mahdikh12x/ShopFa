namespace _01_ShopFaQuery.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStockStatus(CheckStock stock);
    }
}
