namespace _01_ShopFaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetArrivalsProducts();
        ProductQueryModel? GetProduct(string slug);
    }
}
