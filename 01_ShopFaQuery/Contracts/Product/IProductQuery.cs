namespace _01_ShopFaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetArrivalsProducts();
        List<ProductQueryModel> SearchProduct(string value);
        ProductQueryModel? GetProduct(string slug);
        
    }
}
